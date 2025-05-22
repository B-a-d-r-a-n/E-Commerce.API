
global using Domain.Contracts;
global using Domain.Models.Basket;


namespace Persistence.Repositories
{
    internal class BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        : IBasketRepository
    {
        private readonly IDatabase _database=connectionMultiplexer.GetDatabase();
        public async Task<CustomerBasket?> GetAsync(string id)
        {
            // get object from database
            // Deserlization from json
            // return
            var basket = await _database.StringGetAsync(id);

            if (basket.IsNullOrEmpty)
            {
                return null;
            }
            // deserialize to object
            return JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            // serialize to json to store in redis
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated=  await _database.StringSetAsync(basket.Id,jsonBasket,
                timeToLive?? TimeSpan.FromDays(30));
            return  isCreatedOrUpdated ? await GetAsync(basket.Id) : null;

        }
        public async Task<bool> DeleteAsync(string id) => await _database.KeyDeleteAsync(id);


    }
}
