
namespace Domain.Models.Basket
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quanityty { get; set; }
    }
}
