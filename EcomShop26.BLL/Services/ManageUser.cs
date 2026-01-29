using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public class ManageUserService : IManageUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<UserListResponse>> GetUsersAsync()
        {
            var users =await _userManager.Users.ToListAsync();
            var result = users.Adapt<List<UserListResponse>>();
            for (int i = 0; i < users.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(users[i]);
                result[i].Roles = roles.ToList();
            }
            return result;
        }

        public Task<UserDetailsResponse> GetUserDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseRespose> BlockedUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
          
            await _userManager.SetLockoutEnabledAsync(user,true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            await _userManager.UpdateAsync(user);
            return new BaseRespose
            {
                Success = true,
                Message = "user blocked "
            };
        }

        public async Task<BaseRespose> UnBlockedUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, null);

            await _userManager.UpdateAsync(user);
            return new BaseRespose
            {
                Success = true,
                Message = "user Unblocked "
            };
        }
        public async Task<BaseRespose> ChangeUserRoleAsync(ChangeUserRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, request.Role);
            return new BaseRespose
            {
                Success = true,
                Message = "role Updated "
            };
        }


    }
}
