using EventService.Application.Features.Images;
using EventService.Application.Features.Rooms;
using FluentValidation;

namespace EventService.Application.Features.Events.Commands.AddEvent;

internal class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    public AddEventCommandValidator(ImagesService imagesService, RoomsService roomsService)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotEmpty().MinimumLength(4);
        RuleFor(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.RoomId).NotEmpty();
        RuleFor(x => x.RoomId).Must(roomsService.IsRoomExists).WithMessage("Room does not exists.");
        RuleFor(x => x.PreviewImageId).Must(imagesService.IsImageExists).WithMessage("Image does not exists.");
    }
}
