
namespace Domain.Exceptions
{
    public abstract class NotFoundException(string message):
        Exception(message);

    public sealed class AddressNotFoundException(string userName)
    : NotFoundException($"The user {userName} has no address");
    public sealed class ProductNotFoundException(int id)
    : NotFoundException($"Product with Id {id} not found");

    public sealed class UserNotFoundException(string email)
    : NotFoundException($"No User with email {email} was found");

    public sealed class DeliveryMethodNotFoundException(int id) 
        : NotFoundException($"No Delivery Method with Id {id} was found");
}
