using MediatR;

namespace EventsService.Features.Events.UpdateEvent;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
{
    private readonly IEventRepository _eventRepository;

    public async Task<Event> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
    {
        var activity = new Event(
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId,
            command.HasPlaces);

        _eventRepository.Update(activity);

        return activity;
    }

    public UpdateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
