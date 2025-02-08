using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IGuestRepository : IBaseRepository<Guest>
{
    Task<Guest?> GetByCpf(ulong cpf);
    Task<Guest?> GetByCardOfNumber(ulong cardOfNumber);
    Task<List<Guest>> GetInactive();
}