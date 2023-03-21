using MediatR;

namespace EventsService.Features.Tickets.AddTicketsToEvent;

public record AddTicketsToEventCommand(
    Guid EventId,
    int Count) : IRequest;
