using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    Task<T> Add(T entity);
    Task Update(T entity);
    Task<List<T>> Get();
    Task<T?> GetById(long id);
    Task Delete(T entity);

    IUnitOfWork UnitOfWork { get; }
}