
namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException(int id)
        :NotFoundException($"Product with Id {id} not found");
    
    
}
