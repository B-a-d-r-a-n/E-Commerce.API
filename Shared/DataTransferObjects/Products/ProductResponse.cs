
namespace Shared.DataTransferObjects.Products
{
    public record ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string PictureUrl { get; init; }
        public decimal Price { get; init; }
        public string ProductType { get; init; }
        public string ProductBrand { get; init; }
    }
}
