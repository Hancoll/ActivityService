using ActivityService.Application.Common.Interfaces;
using ActivityService.Domain.ActivityAggregate;
using MediatR;

namespace ActivityService.Application.Features.Activities.Commands.DeleteActivity;

internal class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
{
    private readonly IRepository<Activity> _activityReposioty;

    public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        _activityReposioty.Remove(request.Id);
    }

    public DeleteActivityCommandHandler(IRepository<Activity> activityReposioty)
    {
        _activityReposioty = activityReposioty;
    }
}
