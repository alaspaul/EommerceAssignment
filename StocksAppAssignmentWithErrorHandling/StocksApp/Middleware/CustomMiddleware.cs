using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Threading.Tasks;

namespace StocksApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger,
             IDiagnosticContext diagnostic)
        {
            _next = next;
            _logger = logger;
            _diagnosticContext = diagnostic;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {

                await _next(httpContext);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    _logger.LogError("{ExceptionType}/{ExceptionMessage}", e.InnerException, e.InnerException.Message);

                }
                else
                {
                    _logger.LogError("{ExceptionType}/{ExceptionMessage}", e.GetType().ToString(), e.Message);

                }

                //httpContext.Response.StatusCode = 500;
                //await httpContext.Response.WriteAsync("Error Occured");

                throw;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
