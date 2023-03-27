using JetBrains.Annotations;

namespace EventsService.Features.Events;

public record ImageDeleteEvent
{
    [UsedImplicitly]
    public int Type { get; } = 2;

    public Guid DeletedImageId { get; init; }

    [UsedImplicitly]
    public ImageDeleteEvent(Guid deletedImageId)
    {
        DeletedImageId = deletedImageId;
    }
}
