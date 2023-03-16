using ActivityService.Application.Common.Interfaces;
using ActivityService.Application.Features.Images;
using ActivityService.Application.Features.Rooms;
using ActivityService.Domain.Entities;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace ActivityService.Application.Features.Activities.Commands.UpdateActivity;

internal class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator(IRepository<Activity> activityRepository, ImagesService imagesService, RoomsService roomsService)
    {
        RuleFor(x => x.Id).Must(activityRepository.IsExists).WithMessage("Activity does not exists.");
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.RoomId).NotEmpty();
        RuleFor(x => x.RoomId).Must(roomsService.IsRoomExists).WithMessage("Room does not exists.");
        RuleFor(x => x.PreviewImageId).Must(imagesService.IsImageExists).WithMessage("Image does not exists.");
    }
}
