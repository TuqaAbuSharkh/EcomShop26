using Azure.Core;
using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace EcomShop26.PL.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly ICartService _cartService;

        public CartsController(IStringLocalizer<SharedResource> Localizer, ICartService cartService)
        {
            _localizer = Localizer;
            _cartService = cartService;
        }


        [HttpPost("")]
        public async Task<IActionResult> addToCart([FromBody] AddToCartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCartAsync(userId, request);
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.GetUserCartAsync(userId);
            return Ok(result);
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateQuantity([FromRoute]int productId, [FromBody] updateQtRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.UpdateQuantityAsync(userId,productId,request.Count);
            return Ok(result);
        }

        [HttpDelete("")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.ClearCartAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveItem([FromRoute] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.RemoveFromCartAsync(userId,productId);
            return Ok(result);
        }




    }

}
