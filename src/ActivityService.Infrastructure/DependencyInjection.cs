using ActivityService.Application.Common.Interfaces;
using ActivityService.Domain.ActivityAggregate;
using ActivityService.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<Activity>, InMemoryRepository<Activity>>();

        return services;
    }
}
