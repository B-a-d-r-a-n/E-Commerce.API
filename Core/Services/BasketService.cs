

namespace Services
{
    internal class BasketService(IBasketRepository basketRepository,IMapper mapper)
        : IBasketService
    {
        public async Task<bool> DeleteAsync(string id) => 
            await basketRepository.DeleteAsync(id)? true:throw new BasketNotFoundException(id);

        public async Task<BasketDTO> GetAsync(string id)
        {
            // Get Basket 
            // Mapping <Domain , DTO>
            // => Basket DTO 
            var basket = await basketRepository.GetAsync(id)?? throw new BasketNotFoundException(id);
            return mapper.Map<BasketDTO>(basket);
        }

        public async Task<BasketDTO> UpdateAsync(BasketDTO basketDTO)
        {
            var customerBasket=mapper.Map<CustomerBasket>(basketDTO);
            var updatedBasket = await basketRepository.UpdateAsync(customerBasket)
                ?? throw new Exception("Can't update basket now!");
            return mapper.Map<BasketDTO>(updatedBasket);
        }
    }
}
