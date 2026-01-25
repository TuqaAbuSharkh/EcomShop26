using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public interface ICheckoutService
    {
        Task<ChechoutResponse> ProcessPaymentAsync(string userId, CheckoutRequest request);
        Task<ChechoutResponse> handelSuccessAsync(string sessionId);
    }
}
