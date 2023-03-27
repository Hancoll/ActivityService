using JetBrains.Annotations;

namespace EventsService.Features.Events;

public record SpaceDeleteEvent
{
    [UsedImplicitly]
    public int Type { get; } = 1;

    public Guid DeletedSpaceId { get; init; }

    [UsedImplicitly]
    public SpaceDeleteEvent(Guid deletedSpaceId)
    {
        DeletedSpaceId = deletedSpaceId;
    }
}
