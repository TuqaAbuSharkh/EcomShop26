using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EcomShop26.PL.Areas.User
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

        public async Task<IActionResult> indexAsync([FromQuery]string lang ="en")
        {
            var response =await _categoryService.GetAllCategoriesForUser(lang);

            return Ok(new { message = _localizer["Success"].Value, response });
        }

       
    }
}
