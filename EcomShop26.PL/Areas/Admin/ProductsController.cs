using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EcomShop26.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]//(Roles="Admin")
    public class ProductsController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IProductService _productService;

        public ProductsController(IProductService productService,IStringLocalizer<SharedResource> Localizer)
        {
            _localizer = Localizer;
            _productService = productService;
        }
        [HttpPost("")]
       public async Task<IActionResult> Creat([FromForm]ProductRequest request)
        {
            var response = await _productService.CreatProduct(request);
            return Ok(new { message = _localizer["Success"].Value,response });

        }

        [HttpGet("")]

        public async Task<IActionResult> indexAsync()
        {
            var response = await _productService.GetAllProductsForAdmin();

            return Ok(new { message = _localizer["Success"].Value, response });
        }


    }
}
