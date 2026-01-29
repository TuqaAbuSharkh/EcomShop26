using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.Models;
using EcomShop26.PL.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EcomShop26.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IOrderService _orderService;

        public OrdersController(IStringLocalizer<SharedResource> Localizer, IOrderService orderService)
        {
            _localizer = Localizer;
            _orderService = orderService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrders([FromQuery] OrderStauts? stauts)
        {
           var orders = await _orderService.GetOrdersAsync(stauts ?? OrderStauts.Pending);
            return Ok(orders);
        }

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatusAsync([FromRoute] int orderId,[FromBody] OrderStautsRequest request)
        {
            var result = await _orderService.UpdateOrderStautsAsync(orderId, request.OrderStauts);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }


    }
}
