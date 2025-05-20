using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using Shared.DataTransferObjects.ErrorModels;

namespace E_Commerce.Web.MiddleWares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        public CustomExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                //logic

                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {

                await HandleNotFoundPathAsync(httpContext);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //response object
            //set status code  for response
            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //set content type for response 
            httpContext.Response.ContentType = "application/json";
            //response object
            var response = new ErrorDetails()
            {
                StatusCode
            = (int)HttpStatusCode.InternalServerError
            ,
                ErrorMessage = ex.Message
            };

            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound, //tare2a
                UnauthorizedAException => StatusCodes.Status401Unauthorized, 
                BadRequestException badRequestException=> GetValidationErrors(badRequestException,response), 
                _ => (int)HttpStatusCode.InternalServerError, //tare2a tanya
            };
            //return response as json
            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static int GetValidationErrors(BadRequestException badRequestException, ErrorDetails response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundPathAsync(HttpContext httpContext)
        {

                httpContext.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    ErrorMessage = $"Path {httpContext.Request.Path} Not Found"
                    ,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
                await httpContext.Response.WriteAsJsonAsync(response);

        }
    }
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            return app;
        }
    }

}

