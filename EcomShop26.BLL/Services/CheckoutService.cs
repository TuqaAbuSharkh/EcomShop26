using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using EcomShop26.DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepositorycs;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public CheckoutService(ICartRepository cartRepository, IOrderRepository orderRepositorycs
            ,UserManager<ApplicationUser> userManager, IEmailSender emailSender,
            IOrderItemRepository orderItemRepository,IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepositorycs = orderRepositorycs;
            _userManager = userManager;
            _emailSender = emailSender;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }
        public async Task<ChechoutResponse> ProcessPaymentAsync(string userId, CheckoutRequest request)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            if (!cartItems.Any())
            {
                return new ChechoutResponse
                {
                    Success = false,
                    Message = "cart is empty"
                };
            }
            decimal totalamount = 0;


            foreach(var cart in cartItems)
            {
                if(cart.Product.Quantity < cart.Count)
                {
                    return new ChechoutResponse
                    {
                        Success = false,
                        Message = "not enough stock"
                    };
                }
                totalamount += cart.Product.Price * cart.Count;
            }

            Order order = new Order
            {
                UserId = userId,
                PaymentMethod = request.PaymentMethod,
                AmountPaid = totalamount,

            };

            if (request.PaymentMethod == PaymentMethodEnum.cash) {
                return new ChechoutResponse
                {
                    Success = true,
                    Message = "cash"
                };
            }


            else if(request.PaymentMethod == PaymentMethodEnum.vise) {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),

                    Mode = "payment",
                    SuccessUrl = $"https://localhost:7131/api/checkouts/success?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"https://localhost:7131/checkout/cancel",
                    Metadata = new Dictionary<string, string>
                    {
                        {"UserId",userId },
                    }
                };
                foreach (var item in cartItems)
                {
                    options.LineItems.Add(

                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "USD",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Translations.FirstOrDefault(c => c.Language == "en").Name,
                                },
                                UnitAmount = (long)item.Product.Price*100,
                            },
                            Quantity = item.Count,
                        }
                    );
                }

                var service = new SessionService();
                var session = service.Create(options);
                order.sessionId = session.Id;
                await _orderRepositorycs.CreatAsync(order);

                return new ChechoutResponse
                {
                    Success = true,
                    Message = "payment session created",
                    Url= session.Url
                };
            }

            else return new ChechoutResponse
            {
                Success = false,
                Message = "invalid payment method"
            };


        }


        public async Task<ChechoutResponse> handelSuccessAsync(string sessionId)
        {
            var service = new SessionService();
            var session = service.Get(sessionId);
            var userId = session.Metadata["UserId"];

            var order = await _orderRepositorycs.GetBySessionIdAsync(sessionId);
            order.paymentId = session.PaymentIntentId;
            order.OrderStatus = OrderStauts.Approved;
            await _orderRepositorycs.UpdateAsync(order);

            var user = await _userManager.FindByIdAsync(userId);
            var cartItems = await _cartRepository.GetUserCartAsync(userId);
            var orderItems = new List<OrderItem>();
            var productUpdated = new List<(int productId, int quantity)>();

            foreach(var cart in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cart.ProductId,
                    UnitPrice = cart.Product.Price,
                    Quantity = cart.Count,
                    TotalPrice = cart.Count * cart.Product.Price,
                };
                orderItems.Add(orderItem);
                productUpdated.Add((cart.ProductId, cart.Count));
            }
            await _orderItemRepository.CreatAsync(orderItems);
            await _cartRepository.ClearAsync(userId);
            await _productRepository.DecreasQuantityAsync(productUpdated);
            await _emailSender.SendEmailAsync(user.Email, "Payment successfuly", $"<h2>Thank you {user.FullName}</h2>");

            return new ChechoutResponse
            {
                Success = true,
                Message = "payment completed successfuly"
            };
        }
    }

}
