
namespace Domain.Exceptions
{
    public sealed class BadRequestException(List<string> Errors)
        :Exception("Validation failed")
    {
        public List<string> Errors { get; } = Errors;
    }
}
