using MediatR;

namespace EventsService.Features.Events.UpdateEventsAfterImageDeleted;

public record UpdateEventsAfterImageDeletedCommand(
    Guid ImageId) : IRequest;
