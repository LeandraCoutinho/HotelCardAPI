using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IAccessAreaRepository : IBaseRepository<AccessArea>
{
    Task<List<AccessArea>> GetByIds(List<int> accessAreaIds);
}