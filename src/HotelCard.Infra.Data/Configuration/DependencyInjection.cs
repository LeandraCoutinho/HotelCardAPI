using HotelCard.Domain.Contracts.Repositories;
using HotelCard.Infra.Data.Context;
using HotelCard.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HotelCard.Infra.Data.Configuration;

public static class DependencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }

    public static void AddDependencyRepositories(this IServiceCollection service)
    {
        service.AddScoped<IEmployeeRepository, EmployeeRepository>();
        service.AddScoped<IGuestRepository, GuestRepository>();
        service.AddScoped<IAccessAreaRepository, AccessAreaRepository>();
        service.AddScoped<IContractRepository, ContractRepository>();
        service.AddScoped<IRoomRepository, RoomRepository>();
        service.AddScoped<IGuestFlowRepository, GuestFlowRepository>();
        service.AddScoped<IConsumptionProductRepository, ConsumptionProductRepository>();
        service.AddScoped<IConsumptionRepository, ConsumptionRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
    }
}