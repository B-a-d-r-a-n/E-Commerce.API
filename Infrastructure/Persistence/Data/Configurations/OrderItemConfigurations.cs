
namespace Persistence.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.Property(d => d.Price)
                .HasColumnType("decimal(8,2)");
            builder.OwnsOne(builder => builder.Product, productBuilder =>
            {
                productBuilder.WithOwner();
            });
        }
    }
    

}

