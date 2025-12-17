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

        List<CategoryResponse> GetAllCategories();
        CategoryResponse CreatCategory(CategoryRequest  request);
    }
}
