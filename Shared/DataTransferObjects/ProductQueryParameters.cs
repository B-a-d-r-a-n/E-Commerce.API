
using Shared.DataTransferObjects.Products;

namespace Shared.DataTransferObjects
{
    public class ProductQueryParameters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions Options { get; set; }
        public string? Search { get; set; }
    }
}
