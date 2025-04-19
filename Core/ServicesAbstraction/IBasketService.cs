
using Shared.DataTransferObjects.Basket;

namespace ServicesAbstraction
{
    public interface IBasketService
    {
        Task<BasketDTO> GetAsync(string id);
        Task<BasketDTO> UpdateAsync(BasketDTO basket);
        Task<bool> DeleteAsync(string id);


    }
}
