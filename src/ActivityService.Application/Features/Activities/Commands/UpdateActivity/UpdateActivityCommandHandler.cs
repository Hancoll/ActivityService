using ActivityService.Application.Common.Interfaces;
using ActivityService.Application.Features.Activities.Commands.CreateActivity;
using ActivityService.Domain.ActivityAggregate;
using MediatR;

namespace ActivityService.Application.Features.Activities.Commands.UpdateActivity;

internal class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Activity>
{
    private readonly IRepository<Activity> _activityReposioty;

    public async Task<Activity> Handle(UpdateActivityCommand command, CancellationToken cancellationToken)
    {
        var activity = new Activity(
            command.Id,
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId);

        _activityReposioty.Update(activity);

        return activity;
    }

    public UpdateActivityCommandHandler(IRepository<Activity> activityReposioty)
    {
        _activityReposioty = activityReposioty;
    }
}
