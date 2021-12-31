using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config {
    public class DeleveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod> {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder) {
            
            builder.Property(d => d.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}