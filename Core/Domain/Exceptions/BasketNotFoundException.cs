
namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException(string key)
        :NotFoundException($"Basket with key {key} is not found")
    {

    }
}
