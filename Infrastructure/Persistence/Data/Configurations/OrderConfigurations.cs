global using Order = Domain.Models.OrderModels.Order;

namespace Persistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(d=> d.Subtotal)
                .HasColumnType("decimal(8,2)");
            builder.HasMany(o => o.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.OwnsOne(o => o.ShipToAddress, builder => builder.WithOwner());
        }
    }
}
