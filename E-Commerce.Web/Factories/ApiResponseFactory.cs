
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext context) 
        {
           //get the entries in model state that has validation errors
           var errors = context.ModelState
           .Where(m => m.Value.Errors.Any())
           .Select(m => new ValidationError
           {
               Field = m.Key,
               Errors = m.Value.Errors.Select(error => error.ErrorMessage)
           });
            var response = new ValidationErrorResponse { ValidationErrors = errors };
           return new BadRequestObjectResult(response);
         }
    }
}
