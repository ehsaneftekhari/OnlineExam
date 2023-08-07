using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;

namespace OnlineExam.EndPoint.API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch
            {
                throw;
            }
        }
    }

}
