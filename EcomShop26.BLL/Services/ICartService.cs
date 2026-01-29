using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public interface ICartService
    {
        Task<BaseRespose> AddToCartAsync(string userId,AddToCartRequest request);
        Task<CartSummaryResponse> GetUserCartAsync(string userId, string lang = "en");

        Task<BaseRespose> ClearCartAsync(string userId);
        Task<BaseRespose> RemoveFromCartAsync(string userId, int productId);
        Task<BaseRespose> UpdateQuantityAsync(string userId, int productId, int count);
    }
}
