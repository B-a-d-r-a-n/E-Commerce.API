
namespace Domain.Exceptions
{
    public sealed class UserNotFoundException(string email)
        : NotFoundException($"No User with email {email} was found");


}
