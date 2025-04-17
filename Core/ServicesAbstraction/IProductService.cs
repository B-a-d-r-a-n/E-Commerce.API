
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Products;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        // get all
        Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters queryParameters);
        // get one
        Task<ProductResponse> GetProductAsync(int id);
        // get all brands
        Task<IEnumerable<BrandResponse>> GetBrandsAsync();
        // get all types
        Task<IEnumerable<TypeResponse>> GetTypesAsync();
    }
}
