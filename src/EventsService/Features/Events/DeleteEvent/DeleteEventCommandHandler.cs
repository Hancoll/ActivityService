using EventsService.Infrastructure.RabbitMq;
using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Events.DeleteEvent;

[UsedImplicitly]
public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IRabbitMqService _rabbitMqService;

    public Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        _eventRepository.Delete(request.EventId);

        var eventDeleteEvent = new EventDeleteEvent(request.EventId);
        _rabbitMqService.SendMessage(eventDeleteEvent);

        return Task.CompletedTask;
    }

    public DeleteEventCommandHandler(IEventRepository eventRepository, IRabbitMqService rabbitMqService)
    {
        _eventRepository = eventRepository;
        _rabbitMqService = rabbitMqService;
    }
}
