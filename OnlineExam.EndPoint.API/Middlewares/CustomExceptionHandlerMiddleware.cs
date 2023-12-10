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
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ConcatExceptionMessages(ex)));
            }
            catch (ApplicationUnAuthenticateException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ConcatExceptionMessages(ex)));
            }
            catch (ApplicationValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ConcatExceptionMessages(ex)));
            }
            catch (ApplicationSourceNotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ConcatExceptionMessages(ex)));
            }
            catch (OEApplicationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ConcatExceptionMessages(ex)));
            }
            catch (APIValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ConcatExceptionMessages(ex)));
            }
            catch (APIException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(ConcatExceptionMessages(ex)));
            }
            catch
            {
                throw;
            }
        }

        private ExceptionHttpMessagesContainer ConcatExceptionMessages (Exception rootException)
        {
            var result = new List<ApplicationExceptionHttpMessageBody>();

            Exception curent = rootException;

            while (curent != null)
            {
                result.Add(new ApplicationExceptionHttpMessageBody(curent.Message, curent.GetType().Name));
                curent = curent.InnerException;
            }
            return new ExceptionHttpMessagesContainer(result);
        }
    }

    record ExceptionHttpMessagesContainer(IEnumerable<ApplicationExceptionHttpMessageBody> messages);
    record ApplicationExceptionHttpMessageBody(string message, string exceptionType) : HttpMessageBody(message);
    record APIExceptionHttpMessageBody(string message, string exceptionType) : HttpMessageBody(message);
    record HttpMessageBody(string message);
}
