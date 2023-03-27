using JetBrains.Annotations;

namespace EventsService.Features.Events;

public record EventDeleteEvent
{
    [UsedImplicitly]
    public int Type { get; } = 3;

    public Guid DeletedEventId { get; init; }

    [UsedImplicitly]
    public EventDeleteEvent(Guid deletedEventId)
    {
        DeletedEventId = deletedEventId;
    }
}
