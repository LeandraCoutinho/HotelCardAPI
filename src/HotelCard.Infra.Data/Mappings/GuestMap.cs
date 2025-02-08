using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class GuestMap : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    { 
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .HasMaxLength(100);

        builder.Property(g => g.Email)
            .HasMaxLength(100);

        builder.Property(g => g.Cpf)
            .HasMaxLength(11);

        builder.Property(g => g.CellPhone)
            .IsRequired(false);

        builder.Property(g => g.Address)
            .IsRequired(false);

        builder.Property(g => g.IsHolder)
            .IsRequired(false);

        builder.Property(g => g.DateOfBirth);

        builder.Property(g => g.CardOfNumber)
            .IsRequired(false);

        builder.Property(g => g.PhotoUrl)
            .IsRequired(false);

        builder.Property(g => g.IsActive)
            .HasDefaultValue(true);
        
        builder.HasMany(g => g.GuestAccessAreas)
            .WithOne(ga => ga.Guest)
            .HasForeignKey(ga => ga.GuestId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}