using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Tickets.Queries.HasUserTicketToEvent;

internal class HasUserTicketToEventQueryHandler : IRequestHandler<HasUserTicketToEventQuery, bool>
{
    private readonly IRepository<Event> _eventRepository;

    public async Task<bool> Handle(HasUserTicketToEventQuery request, CancellationToken cancellationToken)
    {
        var eventTickets = _eventRepository.GetEntity(request.EventId).Tickets;

        foreach(var ticket in eventTickets)
        {
            if (ticket.Owner == request.UserId)
                return true;
        }

        return false;
    }

    public HasUserTicketToEventQueryHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
