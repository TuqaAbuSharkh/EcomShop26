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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<Category> CreatAsync(Category request)
        {
          await  _context.Categories.AddAsync(request);
           _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.User).Include(c => c.Translations).ToListAsync();
        }

        public async Task<Category?> FindbyIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Translations)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
