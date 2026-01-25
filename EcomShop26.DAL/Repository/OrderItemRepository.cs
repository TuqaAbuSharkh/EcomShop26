using EcomShop26.DAL.Data;
using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task CreatAsync(List<OrderItem> request)
        {
            await _context.OrderItems.AddRangeAsync(request);
            _context.SaveChangesAsync();
        }
    }
}
