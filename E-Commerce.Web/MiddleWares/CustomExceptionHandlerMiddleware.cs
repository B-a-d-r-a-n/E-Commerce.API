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
            }catch (Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                //response object
                //set status code  for response
                //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //set content type for response 
                httpContext.Response.ContentType = "application/json";
                //response object
                var response = new ErrorDetails()
                {
                StatusCode
                =(int)HttpStatusCode.InternalServerError
                ,ErrorMessage= ex.Message
                };
                response.StatusCode = ex switch
                {
                    NotFoundException => (int)HttpStatusCode.NotFound,
                     _ => (int)HttpStatusCode.InternalServerError,
                };
                //return response as json
               httpContext.Response.StatusCode= response.StatusCode;
              await httpContext.Response.WriteAsJsonAsync(response);
            }
            
        }
    }
}
