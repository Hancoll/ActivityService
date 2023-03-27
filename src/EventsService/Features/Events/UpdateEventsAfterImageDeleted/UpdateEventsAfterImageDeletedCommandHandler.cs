using JetBrains.Annotations;
using MediatR;
using MongoDB.Driver;

namespace EventsService.Features.Events.UpdateEventsAfterImageDeleted;

[UsedImplicitly]
public class UpdateEventsAfterImageDeletedCommandHandler : IRequestHandler<UpdateEventsAfterImageDeletedCommand>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventsAfterImageDeletedCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public Task Handle(UpdateEventsAfterImageDeletedCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Event>.Filter.Where(x => x.PreviewImageId == request.ImageId);
        var update = Builders<Event>.Update.Set(x => x.PreviewImageId, null);

        _eventRepository.Update(filter, update);

        return Task.CompletedTask;
    }
}
