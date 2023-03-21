using MediatR;

namespace EventsService.Features.Events.UpdateEvent;

public record UpdateEventCommand(
    Guid Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId,
    bool HasPlaces) : IRequest<Event>;
