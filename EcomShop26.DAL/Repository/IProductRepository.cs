using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreatAsync(Product product);
        Task<List<Product>> GetAllAsync();
        Task<Product?> FindByIdAsync(int id);
        Task<bool> DecreasQuantityAsync(List<(int productId, int quantity)> items);
        IQueryable<Product> Query();
    }
}
