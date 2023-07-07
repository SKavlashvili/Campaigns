using Campaigns.Infrastructure;

namespace Campaigns.WebAPI.Ifrastructure.Middlewares
{
    public class ConnectionDestroyer
    {
        private RequestDelegate _next;
        public ConnectionDestroyer(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            IServiceProvider serviceProvider = context.RequestServices;

            IEnumerable<object> resolvedServices = serviceProvider.GetServices<object>();

            bool contextResolved = resolvedServices.Any(o => o.GetType() == typeof(CampaignsDBContext));

            if (contextResolved) serviceProvider.GetService<CampaignsDBContext>().Connection.Dispose();
        }
    }
}
