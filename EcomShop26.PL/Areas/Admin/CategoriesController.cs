using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

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

        [HttpGet("")]

        public async Task<IActionResult> indexAsync()
        {
            var response = await _categoryService.GetAllCategoriesForAdmin();

            return Ok(new { message = _localizer["Success"].Value, response });
        }



        [HttpPost("")]
       
        public async Task<IActionResult> Creat([FromBody]CategoryRequest request)
        {

            var response =await _categoryService.CreatCategory(request);
            return Ok(new { message = _localizer["Success"].Value });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var result =await  _categoryService.UpdateCategoryAsync(id, request);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            return Ok(result);
        }


        [HttpPatch("toggle-Status/{id}")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int id)
        {
            var result =await _categoryService.ToggelStatus(id);
            if (!result.Success)
            {
                if (result.Message.Contains("not found"))
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }

            return Ok(result);
        }

    }
}
