using EventsService.Features.Tickets;

namespace EventsService.Features.Events;

public record UpdateEventRequest(
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid? PreviewImageId,
    Guid SpaceId,
    List<Ticket> Tickets,
    bool HasPlaces,
    decimal? Price);
