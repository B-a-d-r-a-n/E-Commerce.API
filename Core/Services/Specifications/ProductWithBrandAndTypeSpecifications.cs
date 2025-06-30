
using System.Linq.Expressions;
using Domain.Models.Products;
using Shared.DataTransferObjects;

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
        public ProductWithBrandAndTypeSpecifications(ProductQueryParameters parameters)
        : base(CreateCriteria(parameters)
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            ApplySorting(parameters.Sort);
            ApplyPagination(parameters.PageSize,parameters.PageIndex);
        }

        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return product =>

               (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
               (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value)&&
               (string.IsNullOrWhiteSpace(parameters.Search)
               ||product.Name.ToLower().Contains(parameters.Search.ToLower()));
        }

        private void ApplySorting(ProductSortingOptions options)
        {
            switch (options)
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
