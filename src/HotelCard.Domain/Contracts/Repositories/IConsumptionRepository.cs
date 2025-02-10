using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IConsumptionRepository
{
    Task<Consumption> Add(Consumption consumption);
    Task<IList<Consumption>> GetByGuestId(int guestId);
    Task<IList<Consumption>> GetAll();
    IUnitOfWork UnitOfWork { get; }
}