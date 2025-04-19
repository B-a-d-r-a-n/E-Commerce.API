
using Domain.Models.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //get
        Task<CustomerBasket?> GetAsync(string id);
        //update
        Task<CustomerBasket?> UpdateAsync(CustomerBasket? basket, TimeSpan? timeToLive =null);
        //delete
        Task<bool> DeleteAsync(string id);
    }
}
