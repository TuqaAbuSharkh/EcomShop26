using EcomShop26.BLL.Services;
using EcomShop26.DAL.Data;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using EcomShop26.DAL.Repository;
using EcomShop26.PL.Resourses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EcomShop26.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IStringLocalizer<SharedResource> Localizer, ICategoryService categoryService)
        {
            _localizer = Localizer;
            _categoryService = categoryService;
        }
        [HttpGet("")]

        public IActionResult index()
        {
            var response = _categoryService.GetAllCategories();

            return Ok(new {message = _localizer["Success"].Value,response });
        }

        [HttpPost("")]
        public IActionResult Creat(CategoryRequest request)
        {
            var response = _categoryService.CreatCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
