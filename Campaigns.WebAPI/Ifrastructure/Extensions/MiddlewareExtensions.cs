using Campaigns.WebAPI.Ifrastructure.Middlewares;

namespace Campaigns.WebAPI.Ifrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalErrorHandlerMiddleware>();
        }

        public static IApplicationBuilder UseConnectionDestroyer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ConnectionDestroyer>();
        }
    }
}
