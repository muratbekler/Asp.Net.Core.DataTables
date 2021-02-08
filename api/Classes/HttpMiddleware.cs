using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HttpInterceptor.Classes
{
    public class HttpMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, DbContext dbContext)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("swagger"))
            {
                await _next.Invoke(context);
                return;
            }
            Stream originalBody = null;
            using (MemoryStream modifiedBody = new MemoryStream())
            {
                using (StreamReader streamReader = new StreamReader(modifiedBody))
                {
                    try
                    {
                        context.Response.OnStarting((state) =>
                        {
                            context.Response.ContentType = "application/json";

                            return Task.CompletedTask;
                        }, null);

                        originalBody = context.Response.Body;
                        context.Response.Body = modifiedBody;

                        await _next.Invoke(context);

                        modifiedBody.Seek(0, SeekOrigin.Begin);

                        string originalContent = streamReader.ReadToEnd();
                        context.Response.Body = originalBody;

                        EntityEntry addedEntity = dbContext.ChangeTracker.Entries().Where(e => e.State.Equals(EntityState.Added)).FirstOrDefault();

                        //Transaction Completed
                        await dbContext.SaveChangesAsync();

                        if (context.Response.StatusCode != 204)
                        {

                        }
                    }
                    catch (Exception exception)
                    {
                        if (!(originalBody is null) && !(modifiedBody is null))
                        {
                            modifiedBody?.Seek(0, SeekOrigin.Begin);
                            context.Response.Body = originalBody;
                        }
                        await HandleExceptionAsync(context);
                        throw exception;
                    }
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context)
        {
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ResponseDatum
            {
                Datum = null,
                IsSuccessful = false,
                Message = "Sistem bir hata ile karşılaştı!"
            }));
        }
    }

    public static class HttpMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpMiddleware>();
        }
    }
}
