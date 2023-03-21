using MediatR;

namespace EventsService.Features.Events.DeleteEvent;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        _eventRepository.Delete(request.EventId);
    }

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
