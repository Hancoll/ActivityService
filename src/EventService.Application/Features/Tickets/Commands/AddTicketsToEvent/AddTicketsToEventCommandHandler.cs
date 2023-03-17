using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Tickets.Commands.AddTicketsToEvent;

internal class AddTicketsToEventCommandHandler : IRequestHandler<AddTicketsToEventCommand>
{
    private readonly IRepository<Event> _eventRepository;

    public async Task Handle(AddTicketsToEventCommand request, CancellationToken cancellationToken)
    {
        if (request.Count < 1)
            return;

        var @event = _eventRepository.GetEntity(request.EventId);
        var tickets = new List<Ticket>(request.Count);

        for(var i = 0; i < request.Count; i++)
        {
            tickets.Add(new(Guid.NewGuid(), @event));
        }

        @event.Tickets = tickets;
        @event.HasFreePlaces = true;
    }

    public AddTicketsToEventCommandHandler(IRepository<Event> eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
