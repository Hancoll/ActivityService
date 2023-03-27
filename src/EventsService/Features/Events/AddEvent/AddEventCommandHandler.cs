using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Events.AddEvent;

[UsedImplicitly]
public class AddEventCommandHandler : IRequestHandler<AddEventCommand, Event>
{
    private readonly IEventRepository _eventRepository;

    public Task<Event> Handle(AddEventCommand command, CancellationToken cancellationToken)
    {
        var eventEntity = new Event(
            Guid.NewGuid(),
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId,
            new List<Tickets.Ticket>(),
            command.HasPlaces,
            command.Price);

        _eventRepository.Add(eventEntity);

        return Task.FromResult(eventEntity);
    }

    public AddEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
