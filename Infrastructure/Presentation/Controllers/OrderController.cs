
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
        ///GetAll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll()
        {
            return Ok(await service.OrderService.GetAllAsync(GetEmailFromToken()));
        }
        /// Get
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> Get(Guid id)
        {
            return Ok(await service.OrderService.GetAsync(id));
        }
        /// GetDeliveryMethods
       
        [HttpGet("deliveryMethods"),AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethods()
        {
            return Ok(await service.OrderService.GetDeliveryMethodsAsync());
        }

    }
}
