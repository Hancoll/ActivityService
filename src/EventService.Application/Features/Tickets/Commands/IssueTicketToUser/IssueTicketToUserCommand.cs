using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Tickets.Commands.IssueTicketToUser;

public record IssueTicketToUserCommand(
    Guid UserId,
    Guid EventId) : IRequest<Ticket>;
