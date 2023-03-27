using MediatR;

namespace EventsService.Features.Events.AddEvent;

public record AddEventCommand(
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid? PreviewImageId,
    Guid RoomId,
    bool HasPlaces,
    decimal? Price) : IRequest<Event>;
