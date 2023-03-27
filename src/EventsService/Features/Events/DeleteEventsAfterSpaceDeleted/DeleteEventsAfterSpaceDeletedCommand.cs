using MediatR;

namespace EventsService.Features.Events.DeleteEventsAfterSpaceDeleted;

public record DeleteEventsAfterSpaceDeletedCommand (
    Guid SpaceId) : IRequest;
