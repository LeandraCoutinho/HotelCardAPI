using HotelCard.Infra.Data.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelCard.Application.Configuration;

public static class DependencyInjection
{
    public static void ConfigureApplication(this IServiceCollection service, IConfiguration configuration)
    {
        service.ConfigureDbContext(configuration);
    }
}