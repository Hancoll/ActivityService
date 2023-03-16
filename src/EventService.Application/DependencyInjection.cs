using ActivityService.Application.Common.Behaviors;
using ActivityService.Application.Features.Activities.Commands.CreateActivity;
using ActivityService.Application.Features.Activities.Commands.UpdateActivity;
using ActivityService.Application.Features.Activities.Queries.SearchActivities;
using ActivityService.Application.Features.Images;
using ActivityService.Application.Features.Rooms;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ImagesService>();
        services.AddSingleton<RoomsService>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IValidator<CreateActivityCommand>, CreateActivityCommandValidator>();
        services.AddScoped<IValidator<SearchActivitiesQuery>, SearchActivitiesQueryValidator>();
        services.AddScoped<IValidator<UpdateActivityCommand>, UpdateActivityCommandValidator>();

        // Fix
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
