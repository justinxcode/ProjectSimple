using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ProjectSimple.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
