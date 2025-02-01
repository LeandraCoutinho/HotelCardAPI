using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelCard.Infra.Data.Mappings;

public class ContractRoomMap : IEntityTypeConfiguration<ContractRoom>
{
    public void Configure(EntityTypeBuilder<ContractRoom> builder)
    {
        builder.HasKey(cr => new { cr.ContractId, cr.RoomId });

        builder.HasOne(cr => cr.Contract)
            .WithMany(c => c.ContractRooms)
            .HasForeignKey(cr => cr.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cr => cr.Room)
            .WithMany(r => r.ContractRooms)
            .HasForeignKey(cr => cr.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}