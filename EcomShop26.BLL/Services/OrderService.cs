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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<List<OrderResponse>> GetOrdersAsync(OrderStauts stauts)
        {
            var orders= await _orderRepository.GetOrdersByStautsAsync(stauts);
            return orders.Adapt<List<OrderResponse>>();
        }

        public async Task<BaseRespose> UpdateOrderStautsAsync(int orderId, OrderStauts newStauts)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            order.OrderStatus = newStauts;
            if(newStauts == OrderStauts.Delivered)
            {
                order.PaymentStauts = PaymentStautsEnum.Paid;
            }
            else if (newStauts == OrderStauts.Cancelled)
            {
                if(order.OrderStatus == OrderStauts.Shipped)
                {
                    return new BaseRespose
                    {
                        Success = false,
                        Message = "cant cancel this order"
                    };
                }
            }

            await _orderRepository.UpdateAsync(order);
            return new BaseRespose
            {
                Success = true,
                Message = "order stauts updated"
            };
        }
    }
}
