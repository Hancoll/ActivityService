using MediatR;

namespace EventsService.Features.Tickets.HasUserTicketToEvent;

public record HasUserTicketToEventQuery(
    Guid UserId,
    Guid EventId) : IRequest<bool>;
