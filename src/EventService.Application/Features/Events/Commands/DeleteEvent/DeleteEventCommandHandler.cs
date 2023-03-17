using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Events.Commands.DeleteEvent;

internal class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IRepository<Event> _activityRepository;

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        _activityRepository.Remove(request.EventId);
    }

    public DeleteEventCommandHandler(IRepository<Event> activityRepository)
    {
        _activityRepository = activityRepository;
    }
}
