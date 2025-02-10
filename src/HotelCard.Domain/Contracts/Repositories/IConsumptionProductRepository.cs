using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IConsumptionProductRepository
{
    Task<ConsumptionProduct> Add(ConsumptionProduct consumptionProduct);
    Task AddRange(IEnumerable<ConsumptionProduct> consumptionProducts);
    IUnitOfWork UnitOfWork { get; }
}