
using Shared.Authentication;

namespace Shared.Orders
{
    public record OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset Date { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; } = [];
        public AddressDTO Address { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string PaymentStatus { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty; // Stripe payment intent id
        public decimal Subtotal { get; set; } // subtotal of the order
        public decimal Total { get; set; }
    }
    public record OrderItemDTO
    {
        public string PictureUrl { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
