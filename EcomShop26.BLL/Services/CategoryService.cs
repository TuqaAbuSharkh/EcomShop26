using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using EcomShop26.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task< CategoryResponse> CreatCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
             await _categoryRepository.CreatAsync(category);
            return category.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesForAdmin()
        {
            var categories =await _categoryRepository.GetAllAsync();
      

            var response = categories.Adapt<List<CategoryResponse>>();
            return response;
        }
        public async Task<List<CategoryUserResponse>> GetAllCategoriesForUser(string lang = "en")
        {
            var categories = await _categoryRepository.GetAllAsync();
            var response = categories.BuildAdapter().AddParameters("lang", lang).AdaptToType<List<CategoryUserResponse>>();


            return response;
        }

        public async Task<BaseRespose> ToggelStatus(int id)
        {
            try
            {
                var category = await _categoryRepository.FindbyIdAsync(id);
                if (category is null)
                {
                    return new BaseRespose
                    {
                        Success = false,
                        Message = "category not found "
                    };
                }

                category.Status = category.Status == Status.Active ? Status.InActive : Status.Active;
                await _categoryRepository.UpdateAsync(category);

                return new BaseRespose
                {
                    Success = true,
                    Message = "category updated successfully "
                };

            }
            catch (Exception ex)
            {
                return new BaseRespose
                {
                    Success = false,
                    Message = " can't toggel status",
                    Errors = new List<string> { ex.Message }
                };
            }
        } 

        public async Task<BaseRespose> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.FindbyIdAsync(id);
                if( category is null)
                {
                    return new BaseRespose
                    {
                        Success = false,
                        Message = "category not found "
                    };
                }

                await _categoryRepository.DeleteAsync(category);
                return new BaseRespose
                {
                    Success = true,
                    Message = "category deleted successfully "
                };

            }
            catch(Exception ex)
            {
                return new BaseRespose
                {
                    Success = false,
                    Message = " can't delete category ",
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<BaseRespose> UpdateCategoryAsync(int id, CategoryRequest request)
        {
            try
            {
                var category = await _categoryRepository.FindbyIdAsync(id);
                if (category is null)
                {
                    return new BaseRespose
                    {
                        Success = false,
                        Message = "category not found "
                    };
                }

                if(request.Translations is not null)
                {
                    foreach(var transla in request.Translations)
                    {
                        var existing = category.Translations.FirstOrDefault(t => t.Language == transla.Language);
                        if(existing is not null)
                        {
                            existing.Name = transla.Name;
                        }
                        else
                        {
                            return new BaseRespose
                            {
                                Success = false,
                                Message = $"language {transla.Language} is not supported "
                            };
                        }
                    }
                }

                await _categoryRepository.UpdateAsync(category);
                return new BaseRespose
                {
                    Success = true,
                    Message = "category updated successfully "
                };
            

            }
            catch (Exception ex)
            {
                return new BaseRespose
                {
                    Success = false,
                    Message = " can't delete category ",
                    Errors = new List<string> { ex.Message }
                };
            }
        }


    }
}
