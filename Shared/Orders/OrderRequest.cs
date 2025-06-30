
using Shared.Authentication;

namespace Shared.Orders
{
    public record OrderRequest(string BasketId
        ,AddressDTO ShipToAddress
        , int DeliveryMethodId);
}
