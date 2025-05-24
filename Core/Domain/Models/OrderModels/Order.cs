namespace Domain.Models.OrderModels;
public class Order : BaseEntity<Guid>
{
    public Order()
    {

    }
    public Order(string userEmail,
        ICollection<OrderItem> items,
        OrderAddress address,
        DeliveryMethod deliveryMethod,
         decimal subtotal,
         string paymentIntentId
        )
    {
        BuyerEmail = userEmail;
        Items = items;
        ShipToAddress = address;
        DeliveryMethod = deliveryMethod;
        Subtotal = subtotal;
        PaymentIntentId = paymentIntentId;
    }

    // Id
    public string BuyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public ICollection<OrderItem> Items { get; set; } = [];
    public OrderAddress ShipToAddress { get; set; } = default!;
    public DeliveryMethod DeliveryMethod { get; set; } = default!;
    public int DeliveryMethodId { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string PaymentIntentId { get; set; } = default!;
    public decimal Subtotal { get; set; }
}
