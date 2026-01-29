using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public interface IOrderRepository
    {
        Task<Order> CreatAsync(Order request);
        Task<Order> GetBySessionIdAsync(string sessionId);
        Task<Order> UpdateAsync(Order order);
        Task<List<Order>> GetOrdersByStautsAsync(OrderStauts stauts);

        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<bool> HasUserDeliveredOrderForProduct(string userId, int productId);
    }
}
