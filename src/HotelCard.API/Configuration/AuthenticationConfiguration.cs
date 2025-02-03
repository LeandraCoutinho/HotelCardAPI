using System.Text;
using HotelCard.Core.Enums;
using HotelCard.Core.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HotelCard.API.Configuration;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this IServiceCollection service, IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection("JwtSettings");
        service.Configure<JwtSettings>(appSettingsSection);

        service.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })   .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings")["Key"]!))
            };
        });

        service.AddAuthorization(options =>
        {
            options.AddPolicy(ERole.Administrator.ToString(), builder =>
            {
                builder
                    .RequireAuthenticatedUser()
                    .RequireClaim("UserType", ERole.Administrator.ToString());
            });
            options.AddPolicy(ERole.Operator.ToString(), builder =>
            {
                builder
                    .RequireAuthenticatedUser()
                    .RequireClaim("UserType", ERole.Operator.ToString());
            });
        });

        service
            .AddJwksManager()
            .UseJwtValidation();

        service
            .AddMemoryCache();
    }
}