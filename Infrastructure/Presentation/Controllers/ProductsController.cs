
global using Microsoft.AspNetCore.Mvc;
global using ServicesAbstraction;
global using Shared.DataTransferObjects;
global using Shared.DataTransferObjects.Products;
using Presentation.Attributes;


namespace Presentation.Controllers
{

    public class ProductsController(IServiceManager serviceManager)
        : APIController
    {
        [RedisCash]
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetAllProducts(
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
        //[Authorize(Roles ="Admin")]
        [HttpGet("brands")]
        public async Task<ActionResult<BrandResponse>> GetBrands() // get baseurl/api/products/brands
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
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
