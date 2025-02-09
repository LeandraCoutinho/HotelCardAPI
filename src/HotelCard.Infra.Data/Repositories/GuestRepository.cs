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
    
    public async Task<Guest?> GetById(int id, bool includeAccessAreas = false)
    {
        var query = Context.Guests.AsQueryable();

        if (includeAccessAreas)
        {
            query = query.Include(g => g.GuestAccessAreas)
                .ThenInclude(ga => ga.AccessArea);
        }

        return await query.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Guest?> GetByCpf(ulong cpf)
    {
        var guest = await Context.Guests.FirstOrDefaultAsync(g => g.Cpf == cpf);

        return guest;
    }

    public async Task<Guest?> GetByCardOfNumber(ulong cardOfNumber)
    {
        return await Context.Guests
            .Include(g => g.GuestAccessAreas)
            .FirstOrDefaultAsync(g => g.CardOfNumber == cardOfNumber);
    }

    public async Task<List<Guest>> GetInactive()
    {
        return await Context.Guests.Where
            (g => !g.IsActive)
            .AsNoTracking()
            .ToListAsync();
    }
    public void RemoveGuestAccessAreas(int guestId)
    {
        var guestAccessAreas = Context.GuestAccessAreas
            .Where(gaa => gaa.GuestId == guestId)
            .ToList();

        if (guestAccessAreas.Any())
        {
            Context.GuestAccessAreas.RemoveRange(guestAccessAreas);
        }    
    }

    public async Task<List<Guest>> GetAll()
    {
        return await Context.Guests.Include(g => g.GuestAccessAreas) 
            .ThenInclude(ga => ga.AccessArea) 
            .ToListAsync();
    }
    
    public async Task<Guest> UpdateGuest(Guest guest)
    {
        Context.Guests.Update(guest);
        await Context.SaveChangesAsync(); 
        return guest;
    }
}