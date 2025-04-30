
namespace Shared.DataTransferObjects.ErrorModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }
    }
}
