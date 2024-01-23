using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectSimple.Application.Interfaces;
using ProjectSimple.Infrastructure.DatabaseContext;
using ProjectSimple.Infrastructure.Logging;
using ProjectSimple.Infrastructure.Repositories;

namespace ProjectSimple.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));

        services.AddDbContext<DefaultContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Add additional repositories here

        return services;
    }
}
