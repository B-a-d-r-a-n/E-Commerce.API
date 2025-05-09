
using Shared.Orders;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        // Create (basketId)
        Task<OrderResponse> CreateAsync(OrderRequest request, string email);
        Task<OrderResponse> GetAsync(Guid id);
        Task<IEnumerable<OrderResponse>>  GetAllAsync(string email);
        Task<IEnumerable<DeliveryMethodResponse>>  GetDeliveryMethodsAsync();
    }
}
