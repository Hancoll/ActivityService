using ActivityService.Domain.ActivityAggregate;
using MediatR;

namespace ActivityService.Application.Features.Activities.Commands.CreateActivity
{
    public record CreateActivityCommand(
        DateTime StartDateTime,
        DateTime EndDateTime,
        string Name,
        string Description,
        Guid PreviewImageId,
        Guid RoomId) : IRequest<Activity>;
}
