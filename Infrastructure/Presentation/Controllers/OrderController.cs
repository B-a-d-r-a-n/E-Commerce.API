
using Microsoft.AspNetCore.Authorization;
using Shared.Orders;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager service)
        :APIController
    {
        /// create(address,basketId,deliveryMethodId) => *return* OrderResponse
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderRequest request)
        {
            return Ok(await service.OrderService.CreateAsync(request,GetEmailFromToken()));
        }

        /// GetDeliveryMethods
        /// 

    }
}
