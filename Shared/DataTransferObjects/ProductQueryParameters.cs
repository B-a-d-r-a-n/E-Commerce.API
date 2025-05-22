
using Shared.DataTransferObjects.Products;

namespace Shared.DataTransferObjects
{
    public class ProductQueryParameters
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;

        private int _pageSize = DefaultPageSize;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions Sort { get; set; } 
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize 
        { get => _pageSize;
          set => _pageSize = value > 0 && value < MaxPageSize ? value : DefaultPageSize;
        }
    }
}
