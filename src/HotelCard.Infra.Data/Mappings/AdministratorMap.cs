using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class AdministratorMap : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Email);

        builder.Property(a => a.Password);

        builder.Property(a => a.PasswordTemple);

        builder.Property(a => a.TokenResetPassword)
            .IsRequired(false);
        
        builder.Property(a => a.TokenResetExpiresAt)
            .IsRequired(false);

        builder.Property(a => a.Role)
            .HasConversion<string>();
    }
}