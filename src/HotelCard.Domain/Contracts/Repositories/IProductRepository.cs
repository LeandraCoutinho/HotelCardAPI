using HotelCard.Domain.Entities;

namespace HotelCard.Domain.Contracts.Repositories;

public interface IProductRepository
{
    Task<Product?> GetById(int productId);
}