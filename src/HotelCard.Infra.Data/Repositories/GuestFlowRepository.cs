using HotelCard.Domain.Contracts;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public class GuestFlowRepository : IGuestFlowRepository
{
    private readonly ApplicationDbContext _context;

    public GuestFlowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GuestFlow> Add(GuestFlow guestFlow)
    {
        await _context.GuestFlow.AddAsync(guestFlow);
        return guestFlow;
    }

    public async Task<List<GuestFlow>> GetAll()
    {
        return await _context.GuestFlow
            .Include(gf => gf.Guest)
            .Include(gf => gf.AccessArea)
            .ToListAsync();
    }
    
    public async Task<List<GuestFlow>> GetByCpf(ulong cpf)
    {
        return await _context.GuestFlow
            .Include(gf => gf.Guest) 
            .Include(gf => gf.AccessArea) 
            .Where(gf => gf.Guest.Cpf == cpf) 
            .ToListAsync();
    }
    
    public IUnitOfWork UnitOfWork => _context;
}