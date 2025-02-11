using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IGuestRepository : IBaseRepository<Guest>
{
    Task<Guest?> GetById(int id, bool includeAccessAreas = false);
    Task<Guest?> GetByCpf(ulong cpf);
    Task<Guest?> GetByCardOfNumber(ulong cardOfNumber);
    Task<Guest?> GetByEmail(string email);
    Task<List<Guest>> GetInactive();
    void RemoveGuestAccessAreas(int guestId);
    Task<List<Guest>> GetAll();
}