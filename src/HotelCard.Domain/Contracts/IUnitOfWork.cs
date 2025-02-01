namespace HotelCard.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}