using EventsService.Features.Events;
using EventsService.Infrastructure.Persistence;

namespace EventsService.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        services.AddSingleton<IApplicationContext, ApplicationContext>();

        services.AddSingleton<IEventRepository, EventRepository>();

        return services;
    }
}

