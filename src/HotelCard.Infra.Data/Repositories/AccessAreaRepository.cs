using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public class AccessAreaRepository : BaseRepository<AccessArea>, IAccessAreaRepository
{
    public AccessAreaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<AccessArea>> GetByIds(List<int> accessAreaIds)
    {
        return await Context.AccessAreas.Where(
                a => accessAreaIds.Contains(a.Id))
            .AsNoTracking()
            .ToListAsync();
    }
}