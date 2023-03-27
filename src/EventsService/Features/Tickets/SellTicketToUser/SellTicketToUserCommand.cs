using MediatR;

namespace EventsService.Features.Tickets.SellTicketToUser;

public record SellTicketToUserCommand(
    Guid UserId,
    Guid EventId,
    int? Place) : IRequest<Ticket>;