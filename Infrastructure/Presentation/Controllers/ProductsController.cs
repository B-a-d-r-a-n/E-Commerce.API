
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Products;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IServiceManager serviceManager)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts(
            [FromQuery]ProductQueryParameters queryParameters) // get baseurl/api/products
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(queryParameters);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProducts(int id) // get baseurl/api/products/{id}
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<BrandResponse>> GetBrands() // get baseurl/api/products/brands
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<BrandResponse>> GetTypes() // get baseurl/api/products/types
        {
            var types = await serviceManager.ProductService.GetTypesAsync();
            return Ok(types);
        }

    }

}
