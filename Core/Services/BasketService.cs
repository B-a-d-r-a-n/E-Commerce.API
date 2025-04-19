
using Domain.Exceptions;
using Domain.Models.Basket;
using Shared.DataTransferObjects.Basket;

namespace Services
{
    internal class BasketService(IBasketRepository basketRepository,IMapper mapper)
        : IBasketService
    {
        public async Task<bool> DeleteAsync(string id) => await basketRepository.DeleteAsync(id);

        public async Task<BasketDTO> GetAsync(string id)
        {
            var basket = await basketRepository.GetAsync(id)?? throw new BasketNotFoundException(id);
            return mapper.Map<BasketDTO>(basket);
        }

        public async Task<BasketDTO> UpdateAsync(BasketDTO basket)
        {
            var customerBasket=mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await basketRepository.UpdateAsync(customerBasket)
                ?? throw new Exception("Can't update basket now!");
            return mapper.Map<BasketDTO>(updatedBasket);
        }
    }
}
