using EventsService.Features.Tickets;

namespace EventsService.Features.Events;

public record AddEventRequest(
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid? PreviewImageId,
    Guid SpaceId,
    bool HasPlaces,
    decimal? Price);
