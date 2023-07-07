using Campaigns.Services;
using System.Text.Json;

namespace Campaigns.WebAPI.Ifrastructure.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        private static JsonSerializerOptions _serializerOptions;
        private RequestDelegate _next;
        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        static GlobalErrorHandlerMiddleware()
        {
            _serializerOptions = new JsonSerializerOptions() { WriteIndented = true};
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(BaseResponseException responseEx)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = responseEx.GetStatusCode();
                string res = JsonSerializer.Serialize(new { Message = responseEx.GetMessage(), StatusCode = responseEx.GetStatusCode() },_serializerOptions);
                await context.Response.WriteAsync(res);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                string res = JsonSerializer.Serialize(new { Message = ex.Message, StatusCode = 500},_serializerOptions);
                await context.Response.WriteAsync(res);
                Console.WriteLine(ex.StackTrace);//Delete
            }
        }
    }
}
