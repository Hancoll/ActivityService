using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Events.Commands.UpdateEvent;

public record UpdateEventCommand(
    Guid Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId) : IRequest<Event>;
