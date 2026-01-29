using Amazon.IdentityManagement.Model;
using EcomShop26.BLL.Services;
using EcomShop26.DAL.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcomShop26.PL.Areas.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = """Admin""")]
    public class ManagesController : ControllerBase
    {
        private readonly IManageUserService _ManageUserService;

        public ManagesController(IManageUserService ManageUserService)
        {
            _ManageUserService = ManageUserService;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _ManageUserService.GetUsersAsync();
            return Ok(result);
        }

        [HttpPatch("block/{userd}")]
        public async Task<IActionResult> BlockUser([FromRoute] string userId)
        =>  Ok(await _ManageUserService.BlockedUserAsync(userId));

        [HttpPatch("Unblock/{userd}")]
        public async Task<IActionResult> UnBlockUser([FromRoute] string userId)
        => Ok(await _ManageUserService.UnBlockedUserAsync(userId));

        [HttpPatch("change-role")]
        public async Task<IActionResult> ChangeUserRole(ChangeUserRoleRequest request)
        => Ok(await _ManageUserService.ChangeUserRoleAsync(request));



    }
}
