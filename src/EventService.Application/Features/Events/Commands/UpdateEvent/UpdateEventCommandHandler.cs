using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Events.Commands.UpdateEvent;

internal class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
{
    private readonly IRepository<Event> _activityRepository;

    public async Task<Event> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
    {
        var activity = new Event(
            command.Id,
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId);

        _activityRepository.Update(activity);

        return activity;
    }

    public UpdateEventCommandHandler(IRepository<Event> activityRepository)
    {
        _activityRepository = activityRepository;
    }
}
