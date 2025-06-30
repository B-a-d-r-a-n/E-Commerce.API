
namespace Domain.Models.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } // Created from client
        public ICollection<BasketItem> Items { get; set; } = [];
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
