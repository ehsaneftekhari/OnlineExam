using Newtonsoft.Json;
using OnlineExam.Application.Contract.Exceptions;
using OnlineExam.EndPoint.API.Exceptions;
using System.Net;

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
            catch (ApplicationUnAuthorizedException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApplicationExceptionHttpMessageBody(ex.Message)));
            }
            catch (ApplicationUnAuthenticateException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApplicationExceptionHttpMessageBody(ex.Message)));
            }
            catch (ApplicationValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApplicationExceptionHttpMessageBody(ex.Message)));
            }
            catch (ApplicationSourceNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApplicationExceptionHttpMessageBody(ex.Message)));
            }
            catch (OEApplicationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ApplicationExceptionHttpMessageBody(ex.Message)));
            }
            catch (APIValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new APIExceptionHttpMessageBody(ex.Message)));
            }
            catch (APIException ex)
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

    record ApplicationExceptionHttpMessageBody(string message);
    record APIExceptionHttpMessageBody(string message);
}
