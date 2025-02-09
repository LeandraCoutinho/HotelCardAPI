using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IContractRepository : IBaseRepository<Contract>
{
    Task<Contract?> GetById(int id, bool includeDetails = false);
}