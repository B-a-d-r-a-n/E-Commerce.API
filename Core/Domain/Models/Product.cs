
namespace Domain.Models
{
    public class Product :BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public ProductBrand ProductBrand { get; set; } //reference nav property for brand
        public int BrandId { get; set; }//fk
        public ProductType ProductType { get; set; } //reference navigation property for type
        public int TypeId { get; set; }//fk

    }
}
