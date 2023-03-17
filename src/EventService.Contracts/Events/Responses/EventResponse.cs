using EventService.Contracts.Tickets.Responses;

namespace EventService.Contracts.Events.Responses;

public record EventResponse(
    Guid Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId,
    IEnumerable<TicketResponse> Tickets,
    bool HasFreePlaces);

