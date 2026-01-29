using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Security.Claims;

namespace EcomShop26.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;

        public ProductsController(IStringLocalizer<SharedResource> Localizer, IProductService productService
            ,IReviewService reviewService)
        {
            _localizer = Localizer;
            _productService = productService;
            _reviewService = reviewService;
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

        [HttpPost("{productId}/reviews")]
        public async Task<IActionResult> CreatReview([FromRoute]int productId, [FromBody]CreatReveiwRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _reviewService.AddReviewAsync(userId, productId, request);
            if (!response.Success)
                return BadRequest(new {message =  response.Message });
            return Ok(new { message = response.Message });
        }
    }
}
