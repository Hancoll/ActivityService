using EventsService.Features.Events;
using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Tickets.HasUserTicketToEvent;

[UsedImplicitly]
public class HasUserTicketToEventQueryHandler : IRequestHandler<HasUserTicketToEventQuery, bool>
{
    private readonly IEventRepository _eventRepository;

    public Task<bool> Handle(HasUserTicketToEventQuery request, CancellationToken cancellationToken)
    {
        var eventTickets = _eventRepository.GetEvent(request.EventId).Tickets;

        foreach (var ticket in eventTickets)
        {
            if (ticket.Owner == request.UserId)
                return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public HasUserTicketToEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
