using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using EcomShop26.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;


        public CartService(IProductRepository productRepository,ICartRepository cartRepository)
        {
           _productRepository = productRepository;
            _cartRepository = cartRepository;
        }
        public async Task<BaseRespose> AddToCartAsync(string userId, AddToCartRequest request)
        {
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            if(product is null)
            {
                return new BaseRespose
                {
                    Success = false,
                    Message = "product Not found"
                };
            }
            if(product.Quantity < request.Count)
            {
                return new BaseRespose
                {
                    Success = false,
                    Message = "not enough stock"
                };
            }

            var cartItem = await _cartRepository.GetCartItemAsync(userId, request.ProductId);
            if(cartItem is not null)
            {
               cartItem.Count += request.Count;
                await _cartRepository.UpdateAsync(cartItem);
            }
            else
            {
                var cart = request.Adapt<Cart>();
                cart.userId = userId;

                await _cartRepository.CreatAsync(cart);
            }

          
            return new BaseRespose
            {
                Success = true,
                Message = "product added to cart"
            };
        }

        public async Task<CartSummaryResponse> GetUserCartAsync(string userId, string lang = "en")
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);

            var items = cartItems.Select(c => new CartRespnse
            {
                productId = c.ProductId,
                productName = c.Product.Translations.FirstOrDefault(t=>t.Language == lang).Name,
                Count = c.Count,
                Price = c.Product.Price
            }).ToList();

            return new CartSummaryResponse
            {
                Items = items
            };
            
        }

        public async Task<BaseRespose> ClearCartAsync(string userId)
        {
            await _cartRepository.ClearAsync(userId);
            return new BaseRespose
            {
                Success = true,
                Message = "cart cleared succussfully"
            };
        }




    }
}
