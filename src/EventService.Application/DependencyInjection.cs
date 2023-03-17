using EventService.Application.Common.Behaviors;
using EventService.Application.Features.Events.Commands.AddEvent;
using EventService.Application.Features.Events.Commands.UpdateEvent;
using EventService.Application.Features.Events.Queries.GetEvents;
using EventService.Application.Features.Images;
using EventService.Application.Features.Rooms;
using EventService.Application.Features.Tickets.Commands.IssueTicketToUser;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ImagesService>();
        services.AddSingleton<RoomsService>();

        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IValidator<AddEventCommand>, AddEventCommandValidator>();
        services.AddScoped<IValidator<GetEventsQuery>, GetEventsQueryValidator>();
        services.AddScoped<IValidator<UpdateEventCommand>, UpdateEventCommandValidator>();
        services.AddScoped<IValidator<IssueTicketToUserCommand>, IssueTicketToUserCommandValidator>();

        // Fix
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
