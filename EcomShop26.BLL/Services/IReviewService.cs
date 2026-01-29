using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public interface IReviewService
    {
        Task<BaseRespose> AddReviewAsync(string userId, int productId, CreatReveiwRequest request);
    }
}
