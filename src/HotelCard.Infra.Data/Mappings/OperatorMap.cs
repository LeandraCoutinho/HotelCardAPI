using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class OperatorMap : IEntityTypeConfiguration<Operator>
{
    public void Configure(EntityTypeBuilder<Operator> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Email);

        builder.Property(o => o.Password);

        builder.Property(o => o.PasswordTemple);

        builder.Property(o => o.TokenResetPassword)
            .IsRequired(false);
        
        builder.Property(o => o.TokenResetExpiresAt)
            .IsRequired(false);

        builder.Property(o => o.Role)
            .HasConversion<string>();
    }
}