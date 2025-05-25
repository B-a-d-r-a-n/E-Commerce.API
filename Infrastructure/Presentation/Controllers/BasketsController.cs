
global using Shared.DataTransferObjects.Basket;

namespace Presentation.Controllers
{

    public class BasketsController(IServiceManager serviceManager)
        : APIController
    {
        // get basket by id
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> Get(string id)
        {
            var basket =await serviceManager.BasketService.GetAsync(id);
            return Ok(basket);
        }
        // update basket ( (BasketDto) => Create Basket , Add item to Basket , Remove iten from basket)
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDto)
        {
            var basket = await serviceManager.BasketService.UpdateAsync(basketDto);
            return Ok(basket);
        }
        // delete basket
        [HttpDelete]
        public async Task<ActionResult<BasketDTO>> Delete(string id)
        {
            await serviceManager.BasketService.DeleteAsync(id);
                return NoContent();
           
        }
    }
}
