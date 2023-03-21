using EventsService.Features.Events;
using MediatR;

namespace EventsService.Features.Tickets.HasUserTicketToEvent;

public class HasUserTicketToEventQueryHandler : IRequestHandler<HasUserTicketToEventQuery, bool>
{
    private readonly IEventRepository _eventRepository;

    public async Task<bool> Handle(HasUserTicketToEventQuery request, CancellationToken cancellationToken)
    {
        var eventTickets = _eventRepository.GetEvent(request.EventId).Tickets;

        foreach (var ticket in eventTickets)
        {
            if (ticket.Owner == request.UserId)
                return true;
        }

        return false;
    }

    public HasUserTicketToEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
