using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EcomShop26.PL.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IStringLocalizer<SharedResource> Localizer, ICategoryService categoryService)
        {
            _localizer = Localizer;
            _categoryService = categoryService;
        }



        [HttpPost("")]
       
        public IActionResult Creat(CategoryRequest request)
        {
            var response = _categoryService.CreatCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
