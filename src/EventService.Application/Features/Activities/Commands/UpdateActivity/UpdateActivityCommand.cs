using ActivityService.Domain.Entities;
using MediatR;

namespace ActivityService.Application.Features.Activities.Commands.UpdateActivity;

public record UpdateActivityCommand(
    Guid Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId) : IRequest<Activity>;
