using MediatR;

namespace EventService.Application.Features.Tickets.Queries.HasUserTicketToEvent;

public record HasUserTicketToEventQuery(
    Guid UserId,
    Guid EventId) : IRequest<bool>;
