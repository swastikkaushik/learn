using Api.Configurations;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services , IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        services.Configure<JwtSettings>(jwtSection);
        var jwtSettings = jwtSection.Get<JwtSettings>();
        if (jwtSettings == null ||
            string.IsNullOrWhiteSpace(jwtSettings.Key) ||
            string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
            string.IsNullOrWhiteSpace(jwtSettings.Audience))
        {
            throw new Exception("JWT configuration is invalid , jwtSettings: " + jwtSettings);
        }



        var databaseSection = configuration.GetSection("Database");
        services.Configure<DatabaseSettings>(databaseSection);
        var databaseSettings = databaseSection.Get<DatabaseSettings>();
        if (databaseSettings == null ||
            string.IsNullOrWhiteSpace(databaseSettings.ConnectionString))
        {
            throw new Exception("Database configuration is invalid");
        }

        return services;
    }
}