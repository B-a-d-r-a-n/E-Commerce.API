

namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork,
        IMapper mapper)
        : IProductService
    {
        public async Task<PaginatedResponse<ProductResponse>> GetAllProductsAsync(
            ProductQueryParameters queryParameters)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParameters);
            var product = await unitOfWork.GetRepository<Product,int>().GetAllAsync(specifications);
            var data= mapper.Map<IEnumerable<Product>,IEnumerable<ProductResponse>>(product);
            var pageCount = data.Count(); // عدد الموجود في الصفحة دي فقط كل مرة بتبعت ريكويست فانت بتاخد الي حددت عدده في الكويري لأن اتعمل سكيب و تيك فانت بتعد الي اتعمله تيك هنا الي مش شرط يكون الرقم الي محدده لو مفيش الي يكمله
            var totalCount =await unitOfWork.GetRepository<Product, int>()
                .CountAsync(new ProductCountSpecifications(queryParameters));
            return new(queryParameters.PageIndex, pageCount,totalCount,data);
        }


        public async Task<ProductResponse> GetProductAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specifications)??
            throw new ProductNotFoundException(id);
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
