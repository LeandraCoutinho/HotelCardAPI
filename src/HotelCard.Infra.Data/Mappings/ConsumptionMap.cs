using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class ConsumptionMap : IEntityTypeConfiguration<Consumption>
{
    public void Configure(EntityTypeBuilder<Consumption> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.DateConsumption)
            .IsRequired();
        
        builder.Property(c => c.PaymentId)
            .IsRequired()
            .HasConversion<string>(); 
        
        builder.HasOne(c => c.Guest)
            .WithMany(g => g.Consumptions)
            .HasForeignKey(c => c.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.ConsumptionProducts)
            .WithOne(cp => cp.Consumption)
            .HasForeignKey(cp => cp.ConsumptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
