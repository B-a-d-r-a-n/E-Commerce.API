
namespace Persistence.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(P => P.BrandId);

            builder.HasOne(p => p.ProductType)
             .WithMany()
             .HasForeignKey(P => P.TypeId);

            builder.Property(p => p.Price).HasColumnType("decimal(8,2)");
        }
    }
}
