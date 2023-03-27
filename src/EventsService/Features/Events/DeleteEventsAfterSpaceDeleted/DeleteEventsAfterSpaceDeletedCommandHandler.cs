using JetBrains.Annotations;
using MediatR;
using MongoDB.Driver;

namespace EventsService.Features.Events.DeleteEventsAfterSpaceDeleted;

[UsedImplicitly]
public class DeleteEventsAfterSpaceDeletedCommandHandler : IRequestHandler<DeleteEventsAfterSpaceDeletedCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventsAfterSpaceDeletedCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public Task Handle(DeleteEventsAfterSpaceDeletedCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Event>.Filter.Where(x => x.SpaceId == request.SpaceId);

        _eventRepository.Delete(filter);

        return Task.CompletedTask;
    }
}
