

using Services.Specifications;
using Shared.DataTransferObjects;

namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork,
        IMapper mapper)
        : IProductService
    {
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(
            ProductQueryParameters queryParameters)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParameters);
            var product = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
            return mapper.Map<IEnumerable<Product>,IEnumerable<ProductResponse>>(product);
        }


        public async Task<ProductResponse> GetProductAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specifications);
            return mapper.Map<Product,ProductResponse>(product);
        }
        public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
        {
            //unit of work -> return ienumerable <TypeResponse>
            // automapper on IEnumerable<ProductTypes> -> IEnumerable<TypeResponse>
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponse>>(brands);
        }

        public async Task<IEnumerable<TypeResponse>> GetTypesAsync()
        {
            //unit of work -> return ienumerable <TypeResponse>
            // automapper on IEnumerable<ProductTypes> -> IEnumerable<TypeResponse>
            var types = await unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(types);
        }
    }
}
