using HotelCard.Domain.Contracts;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public class ConsumptionRepository : IConsumptionRepository
{
    private readonly ApplicationDbContext _context;

    public ConsumptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Consumption> Add(Consumption consumption)
    {
        await _context.Consumptions.AddAsync(consumption);
        return consumption;
    }

    public async Task<IList<Consumption>> GetByGuestId(int guestId)
    {
        return await _context.Consumptions
            .Where(c => c.GuestId == guestId)
            .Include(c => c.ConsumptionProducts)
            .ThenInclude(cp => cp.Product)
            .ToListAsync();
    }

    public async Task<IList<Consumption>> GetAll()
    {
        return await _context.Consumptions
            .Include(c => c.Guest)
            .Include(c => c.ConsumptionProducts)
            .ThenInclude(cp => cp.Product)
            .ToListAsync();
    }

    public IUnitOfWork UnitOfWork => _context;
}