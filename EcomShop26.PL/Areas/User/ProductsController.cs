using EcomShop26.BLL.Services;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace EcomShop26.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IProductService _productService;

        public ProductsController(IStringLocalizer<SharedResource> Localizer, IProductService productService)
        {
            _localizer = Localizer;
            _productService = productService;
        }
        [HttpGet("")]

        public async Task<IActionResult> indexAsync([FromQuery] string lang = "en", [FromQuery]int page =1,
            [FromQuery] int limit= 4, [FromQuery] string? search=null, [FromQuery] int? categoryId = null,
            [FromQuery] decimal? maxPrice = null,[FromQuery] decimal? minPrice = null, 
            [FromQuery] string ? sortBy = null, [FromQuery] bool asc = true)
        {
            var response = await _productService
                .GetAllProductsForUser(lang,page,limit,search,categoryId,maxPrice,minPrice,sortBy,asc);

            return Ok(new { message = _localizer["Success"].Value, response });
        }



        [HttpGet("{id}")]

        public async Task<IActionResult> Details([FromRoute] int id,[FromQuery] string lang = "en")
        {
            var response = await _productService.GetAllProductsDetailsForUser(id,lang);

            return Ok(new { message = _localizer["Success"].Value, response });
        }


    }
}
