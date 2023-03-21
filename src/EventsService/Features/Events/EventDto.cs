using EventsService.Features.Tickets;

namespace EventsService.Features.Events;

public record EventDto(
    Guid Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId,
    IEnumerable<TicketDto> Tickets,
    bool HasFreePlaces);
