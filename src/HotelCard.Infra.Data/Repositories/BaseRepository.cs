using HotelCard.Domain.Contracts;
using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext Context;
    private DbSet<T> _dbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        entity.CreatedAt = DateTime.Now;
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task Update(T entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _dbSet.Entry(entity).State = EntityState.Modified;
        _dbSet.Update(entity);
    }

    public async Task<List<T>> Get()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetById(long id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public IUnitOfWork UnitOfWork => Context;
}