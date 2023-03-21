using MediatR;

namespace EventsService.Features.Tickets.IssueTicketToUser;

public record IssueTicketToUserCommand(
    Guid UserId,
    Guid EventId,
    int? Place) : IRequest<Ticket>;
