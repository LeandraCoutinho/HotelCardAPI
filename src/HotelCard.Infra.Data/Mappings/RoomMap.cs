using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class RoomMap : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.IsReserved)
            .IsRequired();

        builder.Property(r => r.DailyPrice)
            .IsRequired();
        
        builder.HasMany(r => r.ContractRooms)
            .WithOne(cr => cr.Room)
            .HasForeignKey(cr => cr.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}