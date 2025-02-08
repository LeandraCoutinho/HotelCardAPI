using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsCheckAvailability(int roomId)
    { 
        return await Context.Rooms
            .AnyAsync(r => r.Id == roomId && !r.IsReserved);
    }

    public async Task<Room?> GetByRoom(int roomId)
    {
        return await Context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
    }
}