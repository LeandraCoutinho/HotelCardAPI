using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class ContractMap : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BeginAt)
            .IsRequired();

        builder.Property(c => c.FinishAt)
            .IsRequired();

        builder.Property(c => c.HolderId)
            .IsRequired();
        
        builder.Property(c => c.PaymentId)
            .IsRequired()
            .HasConversion<string>(); 

        builder.HasOne(c => c.Holder)
            .WithMany()
            .HasForeignKey(c => c.HolderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Dependents)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.ContractRooms)
            .WithOne(cr => cr.Contract)
            .HasForeignKey(cr => cr.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}