
namespace Domain.Exceptions
{
    public sealed class UnauthorizedAException(string message = "Invalid Email or password")
        :Exception(message);

}
