
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrderController
        :APIController
    {
        /// create(address,basketId,deliveryMethodId) => *return* OrderResponse
        /// GetDeliveryMethods
        /// 
    }
}
