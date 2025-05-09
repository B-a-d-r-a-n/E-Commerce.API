
namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail,
            ICollection<OrderItem> items,
            OrderAddress address,
            DeliveryMethod deliveryMethod,
            decimal subtotal)
        {
            UserEmail = userEmail;
        
            Items = items;
            Address = address;
            DeliveryMethod = deliveryMethod;
            //PaymentIntentId = paymentIntentId;
            Subtotal = subtotal;
        }

        // Id guid
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        public ICollection<OrderItem> Items { get; set; } = [];
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending; // default value
        public string PaymentIntentId { get; set; } = string.Empty; // Stripe payment intent id
        public decimal Subtotal { get; set; } // subtotal of the order

    }
}
