using System.Reflection;
using HotelCard.Application.Contracts;
using HotelCard.Application.Notification;
using HotelCard.Application.Services;
using HotelCard.Core.Settings;
using HotelCard.Domain.Entities;
using HotelCard.Infra.Data.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScottBrady91.AspNetCore.Identity;

namespace HotelCard.Application.Configuration;

public static class DependencyInjection
{
    public static void SetupSettings(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        service.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
    }

    public static void ConfigureApplication(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
        service.ConfigureDbContext(configuration);
        service.AddDependencyRepositories();
    }

    public static void AddDependencyServices(this IServiceCollection service)
    {
        service
            .AddScoped<IPasswordHasher<Employee>, Argon2PasswordHasher<Employee>>();

        service.AddScoped<INotificator, Notificator>();

        service
            .AddScoped<IEmployeeService, EmployeeService>()
            .AddScoped<IEmailService, EmailService>();
    }
}