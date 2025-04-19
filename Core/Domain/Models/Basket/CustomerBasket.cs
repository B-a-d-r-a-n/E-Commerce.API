
namespace Domain.Models.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } // Created from client
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
