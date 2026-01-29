using EcomShop26.DAL.DTOs.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace EcomShop26.PL
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var errorDetails = new ErrorDetails()
            {
                StautsCode = StatusCodes.Status500InternalServerError,
                Message = "server error ...",
                StackTrace = exception.InnerException.Message//in development mode only!!!
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(errorDetails);

            return true;
        }
    }
}
