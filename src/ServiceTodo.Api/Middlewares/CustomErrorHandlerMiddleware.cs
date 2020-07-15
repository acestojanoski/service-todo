using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceTodo.Api.Exceptions;
using ServiceTodo.Api.Models.Response;

namespace ServiceTodo.Api.Middlewares
{
    public static class CustomErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomErrorHandlerMiddleware>();
        }
    }

    public class CustomErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                ErrorResponse errorResponse = new ErrorResponse();

                switch (exception)
                {
                    case NotFoundException _:
                        errorResponse.StatusCode = 404;
                        errorResponse.Message = exception.Message;
                        break;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = errorResponse.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
