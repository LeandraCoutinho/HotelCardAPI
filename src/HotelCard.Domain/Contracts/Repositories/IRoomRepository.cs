using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IRoomRepository : IBaseRepository<Room>
{
    Task<bool> IsCheckAvailability(int roomId);
    Task<Room?> GetByRoom(int roomId);
}