﻿
namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager service)
        :APIController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdate(string basketId)
        {
            return Ok(await service.PaymentService.CreateOrUpdatePaymentIntent(basketId));
        }


        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await service.PaymentService.UpdateOrderPaymentStatusAsync(json,
                Request.Headers["Stripe-Signature"]!);
            return new EmptyResult();
        }
    }
}
