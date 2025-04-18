
using System.Net;

namespace Shared.DataTransferObjects.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; }=(int)HttpStatusCode.BadRequest;
        public string ErrorMessage { get; set; } = "Validation failed";
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }

    public class ValidationError
    {
        public string Field { get; set; } = default!;

        public IEnumerable<string> Errors { get; set; }
    }
}
