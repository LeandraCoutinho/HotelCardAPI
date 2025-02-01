using HotelCard.Infra.Data.Context;
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
}