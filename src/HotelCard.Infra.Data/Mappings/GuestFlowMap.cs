using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class GuestFlowMap : IEntityTypeConfiguration<GuestFlow>
{
    public void Configure(EntityTypeBuilder<GuestFlow> builder)
    {
        builder.HasKey(gf => gf.Id);

        builder.Property(gf => gf.AccessTime)
            .IsRequired();

        builder.HasOne(gf => gf.Guest)
            .WithMany() 
            .HasForeignKey(gf => gf.GuestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(gf => gf.AccessArea)
            .WithMany() 
            .HasForeignKey(gf => gf.AccessAreaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}