using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public interface IReviewRepository
    {
        Task<bool> HasUserReviewProduct(string userId, int productId);
        Task<Review> CreatAsync(Review request);
    }
}
