using EventsService.Features.Tickets;

namespace EventsService.Features.Events;

public record EventRequest(
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid? PreviewImageId,
    Guid RoomId,
    List<Ticket> Tickets,
    bool HasPlaces,
    decimal? Price);
