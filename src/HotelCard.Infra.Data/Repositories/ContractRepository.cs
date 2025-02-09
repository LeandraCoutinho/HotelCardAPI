using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public class ContractRepository : BaseRepository<Contract>, IContractRepository
{
    public ContractRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<Contract?> GetById(int id, bool includeDetails = false)
    {
        var query = Context.Contracts.AsQueryable();

        if (includeDetails)
        {
            query = query
                .Include(c => c.Holder)
                .ThenInclude(h => h.GuestAccessAreas)
                .ThenInclude(ga => ga.AccessArea)
                .Include(c => c.Dependents)
                .ThenInclude(d => d.GuestAccessAreas)
                .ThenInclude(ga => ga.AccessArea)
                .Include(c => c.ContractRooms)
                .ThenInclude(cr => cr.Room);
        }

        return await query.FirstOrDefaultAsync(c => c.Id == id);
    }
}