using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Events.Commands.AddEvent;

internal class AddEventCommandHandler : IRequestHandler<AddEventCommand, Event>
{
    private readonly IRepository<Event> _activityRepository;

    public async Task<Event> Handle(AddEventCommand command, CancellationToken cancellationToken)
    {
        var eventEntity = new Event(
            Guid.NewGuid(),
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId);

        _activityRepository.Insert(eventEntity);

        return eventEntity;
    }

    public AddEventCommandHandler(IRepository<Event> activityRepository)
    {
        _activityRepository = activityRepository;
    }
}
