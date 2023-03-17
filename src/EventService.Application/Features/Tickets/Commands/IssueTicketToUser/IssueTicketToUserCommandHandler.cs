using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Tickets.Commands.IssueTicketToUser;

internal class IssueTicketToUserCommandHandler : IRequestHandler<IssueTicketToUserCommand, Ticket>
{
    private readonly IRepository<Event> _eventRepository;

    public async Task<Ticket> Handle(IssueTicketToUserCommand request, CancellationToken cancellationToken)
    {
        var @event = _eventRepository.GetEntity(request.EventId);
        Ticket ticket = null;
        var hasFreePlaces = false;

        for(var i = 0; i < @event.Tickets.Count; i++)
        {
            if (@event.Tickets[i].Owner is not null)
                continue;

            if(ticket is null)
            {
                ticket = @event.Tickets[i];
                ticket.Owner = request.UserId;
                continue;
            }

            hasFreePlaces = true;
            break;
        }

        @event.HasFreePlaces = hasFreePlaces;
        return ticket;
    }

    public IssueTicketToUserCommandHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
