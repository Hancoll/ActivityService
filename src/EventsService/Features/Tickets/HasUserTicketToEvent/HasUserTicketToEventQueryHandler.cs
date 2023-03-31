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

        return Task.FromResult(eventTickets.Any(ticket => ticket.Owner == request.UserId));
    }

    public HasUserTicketToEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
