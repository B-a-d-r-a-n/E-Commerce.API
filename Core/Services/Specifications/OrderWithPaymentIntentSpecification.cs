
namespace Services.Specifications
{
    internal class OrderWithPaymentIntentSpecification(string paymentIntentId)
        : BaseSpecifications<Order>(Order => Order.PaymentIntentId == paymentIntentId )
    {

    }
}
