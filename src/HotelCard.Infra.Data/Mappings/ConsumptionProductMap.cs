using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class ConsumptionProductMap : IEntityTypeConfiguration<ConsumptionProduct>
{
    public void Configure(EntityTypeBuilder<ConsumptionProduct> builder)
    {
        builder.HasKey(cp => new { cp.ConsumptionId, cp.ProductId });

        builder.HasOne(cp => cp.Consumption)
            .WithMany(c => c.ConsumptionProducts)
            .HasForeignKey(cp => cp.ConsumptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cp => cp.Product)
            .WithMany(p => p.ConsumptionProducts)
            .HasForeignKey(cp => cp.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(cp => cp.Quantity)
            .IsRequired();

        builder.Property(c => c.TotalValue)
            .IsRequired(false);
    }
}
