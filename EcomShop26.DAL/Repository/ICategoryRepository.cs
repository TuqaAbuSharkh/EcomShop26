using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreatAsync(Category request);

        Task<Category?> FindbyIdAsync(int id);
        Task DeleteAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
    }
}
