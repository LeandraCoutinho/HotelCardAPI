using HotelCard.Domain.Contracts;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;

namespace HotelCard.Infra.Data.Repositories;

public class ConsumptionProductRepository : IConsumptionProductRepository
{
    private readonly ApplicationDbContext _context;

    public ConsumptionProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ConsumptionProduct> Add(ConsumptionProduct consumptionProduct)
    {
        await _context.ConsumptionProducts.AddAsync(consumptionProduct);
        return consumptionProduct;
    }

    public async Task AddRange(IEnumerable<ConsumptionProduct> consumptionProducts)
    {
        await _context.ConsumptionProducts.AddRangeAsync(consumptionProducts);
    }
    
    public IUnitOfWork UnitOfWork => _context;
}