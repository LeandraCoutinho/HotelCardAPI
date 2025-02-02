using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<Employee?> Get(string email);
}