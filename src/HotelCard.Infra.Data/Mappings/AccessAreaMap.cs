using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class AccessAreaMap : IEntityTypeConfiguration<AccessArea>
{
    public void Configure(EntityTypeBuilder<AccessArea> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.PhotoUrl)
            .HasMaxLength(200);
        
        builder.HasMany(a => a.GuestAccessAreas)
            .WithOne(ga => ga.AccessArea)
            .HasForeignKey(ga => ga.AccessAreaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}