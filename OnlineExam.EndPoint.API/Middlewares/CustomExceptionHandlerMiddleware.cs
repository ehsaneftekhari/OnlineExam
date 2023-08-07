using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineExam.EndPoint.API.Exceptions;
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
            catch(APIException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new APIExceptionHttpMessageBody(ex.Message)));
            }
            catch
            {
                throw;
            }
        }
    }

    record APIExceptionHttpMessageBody(string message);
}
