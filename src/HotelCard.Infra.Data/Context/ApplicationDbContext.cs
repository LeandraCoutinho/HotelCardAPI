using System.Reflection;
using HotelCard.Domain.Contracts;
using HotelCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelCard.Infra.Data.Context;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<AccessArea> AccessAreas { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Consumption> Consumptions { get; set; }

    public DbSet<ConsumptionProduct> ConsumptionProducts { get; set; }
    
    public DbSet<Contract> Contracts { get; set; }

    public DbSet<ContractRoom> ContractRooms { get; set; }

    public DbSet<Guest> Guests { get; set; }

    public DbSet<GuestAccessArea> GuestAccessAreas { get; set; }
    
    public DbSet<Product> Products { get; set; }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<GuestFlow> GuestFlow { get; set; }
    
    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}