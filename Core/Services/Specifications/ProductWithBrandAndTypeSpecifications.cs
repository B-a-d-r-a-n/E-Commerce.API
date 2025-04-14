
using System.Linq.Expressions;
using Domain.Models;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
    {
        // use the constructor to create query to get product by id
        public ProductWithBrandAndTypeSpecifications(int id)
            : base(product => product.Id == id)
        {
            //add includes
            AddInclude(p=> p.ProductBrand);
            AddInclude(p=> p.ProductType);
        }
        // use the constructor to create query to get all product
        // use for sorting and filtration
        public ProductWithBrandAndTypeSpecifications(int? brandId,int? typeId, ProductSortingOptions options)
        :base(product =>
            (!brandId.HasValue || product.BrandId == brandId.Value) &&
            (!typeId.HasValue || product.TypeId == typeId.Value)
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch(options)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break; 
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;  
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;

            }
        }
    }
}
