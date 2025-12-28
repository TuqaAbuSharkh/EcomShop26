using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public interface ICategoryService
    {

        Task<List<CategoryUserResponse>> GetAllCategoriesForUser(string lang = "en");
        Task<List<CategoryResponse>> GetAllCategoriesForAdmin();
        Task<CategoryResponse> CreatCategory(CategoryRequest request);

        Task<BaseRespose> ToggelStatus(int id);
        Task<BaseRespose> DeleteCategoryAsync(int id);
        Task<BaseRespose> UpdateCategoryAsync(int id, CategoryRequest request);
    }
}
