

namespace Persistence.Data.Configurations
{
    public class DeliverMethodsConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(d => d.Price)
                .HasColumnType("decimal(8,2)");

        }
    }
}
