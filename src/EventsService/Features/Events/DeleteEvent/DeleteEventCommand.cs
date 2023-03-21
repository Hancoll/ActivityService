using MediatR;

namespace EventsService.Features.Events.DeleteEvent;

public record DeleteEventCommand(
    Guid EventId) : IRequest;
