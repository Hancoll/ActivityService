using MediatR;

namespace EventsService.Features.Events.AddEvent;

public class AddEventCommandHandler : IRequestHandler<AddEventCommand, Event>
{
    private readonly IEventRepository _eventRepository;

    public async Task<Event> Handle(AddEventCommand command, CancellationToken cancellationToken)
    {
        var eventEntity = new Event(
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId,
            command.HasPlaces);

        _eventRepository.Add(eventEntity);

        return eventEntity;
    }

    public AddEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
