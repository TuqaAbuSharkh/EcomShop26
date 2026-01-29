using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public interface IManageUserService
    {
        Task<List<UserListResponse>> GetUsersAsync();
        Task<UserDetailsResponse> GetUserDetailsAsync();
        Task<BaseRespose> BlockedUserAsync(string userId);
        Task<BaseRespose> UnBlockedUserAsync(string userId);
        Task<BaseRespose> ChangeUserRoleAsync(ChangeUserRoleRequest request);


    }
}
