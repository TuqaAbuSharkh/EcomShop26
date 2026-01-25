using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public interface IProductService
    {
        Task<ProductResponse> CreatProduct(ProductRequest request);
        Task<List<ProductResponse>> GetAllProductsForAdmin();
        Task<List<ProductUserResponse>> GetAllProductsForUser(string lang = "en", int page = 1, int limit = 4,string? search = null);
        Task<ProductUserDetails> GetAllProductsDetailsForUser(int id, string lang = "en");
    }
}
