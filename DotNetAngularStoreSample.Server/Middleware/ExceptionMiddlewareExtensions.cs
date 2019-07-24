using System.Threading.Tasks;
using DotNetAngularStoreSample.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace DotNetAngularStoreSample.Server.Middleware
{
    /// <summary>
    /// Middleware for all exception processing. Uses ExceptionHandlerService to log and serialize the exception, and send it back to client
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ExceptionHandlerService exceptionHandlerService)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context => await HandleException(context, exceptionHandlerService)); ;
            });
        }

        private static async Task HandleException(HttpContext context, ExceptionHandlerService exceptionHandlerService)
        {
            context.Response.ContentType = "application/json";
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                context.Response.StatusCode = exceptionHandlerService.GetStatusCode(contextFeature.Error);
                var json = exceptionHandlerService.LogAndFormatToJson(contextFeature.Error);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
