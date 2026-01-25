using EcomShop26.DAL.Data;
using EcomShop26.DAL.Migrations;
using EcomShop26.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public class CartRepository : ICartRepository
    {

        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> CreatAsync(Cart request)
        {
            await _context.Carts.AddAsync(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<Cart>> GetUserCartAsync(string userId)
        {
            return await _context.Carts
                .Where(c => c.userId == userId)
                .Include(c => c.Product)
                .ThenInclude(c => c.Translations)
                .ToListAsync();
        }


        public async Task<Cart?> GetCartItemAsync(string userId,int productId)
        {
            return await _context.Carts.Include(c => c.ProductId == productId)
                .FirstOrDefaultAsync(c => c.userId == userId && c.ProductId == productId); 
        }

        public async Task<Cart> UpdateAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task ClearAsync(string userId)
        {
            var items = await _context.Carts.Where(c => c.userId == userId).ToListAsync();
            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

    }
}
