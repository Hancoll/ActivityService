using EventsService.Services;
using FluentValidation;

namespace EventsService.Features.Events.AddEvent;

public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    public AddEventCommandValidator(IImagesService imagesService, IRoomsService roomsService)
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
