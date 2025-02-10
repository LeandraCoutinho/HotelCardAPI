using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;

namespace HotelCard.Infra.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetById(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }
}