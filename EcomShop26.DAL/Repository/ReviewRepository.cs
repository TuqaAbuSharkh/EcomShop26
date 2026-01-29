using EcomShop26.DAL.Data;
using EcomShop26.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> HasUserReviewProduct(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(u => u.userId == userId && u.ProductId == productId);
        }

        public async Task<Review> CreatAsync(Review request)
        {
            await _context.Reviews.AddAsync(request);
            _context.SaveChangesAsync();
            return request;
        }
    }
}
