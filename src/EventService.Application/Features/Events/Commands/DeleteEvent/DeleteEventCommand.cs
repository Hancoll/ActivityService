using MediatR;

namespace EventService.Application.Features.Events.Commands.DeleteEvent;

public record DeleteEventCommand(
    Guid EventId) : IRequest;
