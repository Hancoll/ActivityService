using EventsService.Features.Tickets;
using MediatR;

namespace EventsService.Features.Events.UpdateEvent;

public record UpdateEventCommand(
    Guid Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid? PreviewImageId,
    Guid RoomId,
    List<Ticket> Tickets,
    bool HasPlaces,
    decimal? Price) : IRequest<Event>;
