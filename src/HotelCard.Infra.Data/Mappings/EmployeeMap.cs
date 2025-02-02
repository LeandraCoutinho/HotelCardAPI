using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class EmployeeMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name);

        builder.Property(e => e.Email);

        builder.Property(e => e.Password);
        
        builder.Property(e => e.TokenResetPassword)
            .IsRequired(false);
        
        builder.Property(e => e.TokenResetExpiresAt)
            .IsRequired(false);

        builder.Property(e => e.Role)
            .HasConversion<string>();
    }
}