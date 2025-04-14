
namespace Services.MappingProfiles
{
    internal class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(d=> d.BrandName,
                options => 
                options
                .MapFrom(s=>s.ProductBrand.Name))

                   .ForMember(d => d.TypeName,
                options =>
                options
                .MapFrom(s => s.ProductType.Name)); ;
            CreateMap<ProductBrand, BrandResponse>();
            CreateMap<ProductType, TypeResponse>();
        }
    }
}
