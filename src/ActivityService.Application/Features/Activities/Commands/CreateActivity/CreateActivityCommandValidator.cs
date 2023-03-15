using ActivityService.Application.Features.Images;
using ActivityService.Application.Features.Rooms;
using FluentValidation;

namespace ActivityService.Application.Features.Activities.Commands.CreateActivity;

internal class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator(ImagesService imagesService, RoomsService roomsService)
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.RoomId).NotEmpty();
        RuleFor(x => x.RoomId).Must(roomsService.IsRoomExists).WithMessage("Room does not exists.");
        RuleFor(x => x.PreviewImageId).Must(imagesService.IsImageExists).WithMessage("Image does not exists.");
    }
}
