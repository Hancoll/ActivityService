using EventsService.Features.Events;
using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Tickets.AddTicketsToEvent;

[UsedImplicitly]
public class AddTicketsToEventCommandHandler : IRequestHandler<AddTicketsToEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public Task Handle(AddTicketsToEventCommand request, CancellationToken cancellationToken)
    {
        var @event = _eventRepository.GetEvent(request.EventId);

        var tickets = new List<Ticket>(request.Count);

        for (var i = 0; i < request.Count; i++)
        {
            var ticket = new Ticket();

            if (@event.HasPlaces)
                ticket.Place = i + 1 ;

            tickets.Add(ticket);
        }

        @event.Tickets = tickets;
        _eventRepository.Update(@event);

        return Task.CompletedTask;
    }

    public AddTicketsToEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
