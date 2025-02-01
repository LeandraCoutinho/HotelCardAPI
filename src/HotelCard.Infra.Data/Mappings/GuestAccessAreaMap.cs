using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class GuestAccessAreaMap : IEntityTypeConfiguration<GuestAccessArea>
{
    public void Configure(EntityTypeBuilder<GuestAccessArea> builder)
    {
        builder.HasKey(ga => new { ga.GuestId, ga.AccessAreaId });

        builder.HasOne(ga => ga.Guest)
            .WithMany(g => g.GuestAccessAreas)
            .HasForeignKey(ga => ga.GuestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ga => ga.AccessArea)
            .WithMany(a => a.GuestAccessAreas)
            .HasForeignKey(ga => ga.AccessAreaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}