using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using EcomShop26.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<BaseRespose> AddReviewAsync(string userId,int productId, CreatReveiwRequest request)
        {
            var hasDelivered = await _orderRepository.HasUserDeliveredOrderForProduct(userId, productId);

            if (!hasDelivered)
            {
                return new BaseRespose
                {
                    Success = false,
                    Message = "you can only review product you recived"
                };
            }
            var hasReview = await _reviewRepository.HasUserReviewProduct(userId, productId);
            if (hasReview)
            {
                return new BaseRespose
                {
                    Success = false,
                    Message = "you can only review product once"
                };
            }
            var review = request.Adapt<Review>();
            review.userId = userId;
            review.ProductId = productId;
            await _reviewRepository.CreatAsync(review);
            return new BaseRespose
            {
                Success = true,
                Message = "review created"
            };
        }
    }
}
