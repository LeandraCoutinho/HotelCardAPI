using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public class GuestRepository : BaseRepository<Guest>, IGuestRepository
{
    public GuestRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Guest?> GetByCpf(ulong cpf)
    {
        var guest = await Context.Guests.FirstOrDefaultAsync(g => g.Cpf == cpf);

        return guest;
    }

    public async Task<Guest?> GetByCardOfNumber(ulong cardOfNumber)
    {
        return await Context.Guests.FirstOrDefaultAsync(g => g.CardOfNumber == cardOfNumber);
    }

    public async Task<List<Guest>> GetInactive()
    {
        return await Context.Guests.Where
            (g => !g.IsActive)
            .AsNoTracking()
            .ToListAsync();
    }
}