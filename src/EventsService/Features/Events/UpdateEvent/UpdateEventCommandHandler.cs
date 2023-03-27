using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Events.UpdateEvent;

[UsedImplicitly]
public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
{
    private readonly IEventRepository _eventRepository;

    public Task<Event> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
    {
        var @event = new Event(
            command.Id,
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId,
            command.Tickets,
            command.HasPlaces,
            command.Price);

        _eventRepository.Update(@event);

        return Task.FromResult(@event);
    }

    public UpdateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
