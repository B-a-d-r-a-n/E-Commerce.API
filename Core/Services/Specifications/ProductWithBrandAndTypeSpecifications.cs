
using System.Linq.Expressions;

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
        public ProductWithBrandAndTypeSpecifications()
            :base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
