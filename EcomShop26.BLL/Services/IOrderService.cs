using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetOrdersAsync(OrderStauts stauts);
        Task<BaseRespose> UpdateOrderStautsAsync(int orderId,OrderStauts newStauts);

        Task<Order?> GetOrderByIdAsync(int orderId);

    }
}
