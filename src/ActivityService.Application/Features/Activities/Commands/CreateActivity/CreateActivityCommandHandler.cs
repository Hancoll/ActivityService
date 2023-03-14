using ActivityService.Application.Common.Interfaces;
using ActivityService.Domain.ActivityAggregate;
using MediatR;
using System.Reflection.Metadata;

namespace ActivityService.Application.Features.Activities.Commands.CreateActivity;

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Activity>
{
    private readonly IRepository<Activity> _activityReposioty;

    public async Task<Activity> Handle(CreateActivityCommand command, CancellationToken cancellationToken)
    {
        var activity = new Activity(
            Guid.NewGuid(),
            command.StartDateTime,
            command.EndDateTime,
            command.Name,
            command.Description,
            command.PreviewImageId,
            command.RoomId);

        _activityReposioty.Insert(activity);

        return activity;
    }

    public CreateActivityCommandHandler(IRepository<Activity> activityReposioty)
    {
        _activityReposioty = activityReposioty;
    }
}
