using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public interface ICartRepository
    {
        Task<Cart> CreatAsync(Cart request);

        Task<List<Cart>> GetUserCartAsync(string userId);
        Task<Cart?> GetCartItemAsync(string userId, int productId);
        Task ClearAsync(string userId);
        Task<Cart> UpdateAsync(Cart cart);
        Task DeleteAsync(Cart cart);
    }
}
