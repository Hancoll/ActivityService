using EventsService.Services;
using FluentValidation;
using JetBrains.Annotations;

namespace EventsService.Features.Events.UpdateEvent;

[UsedImplicitly]
public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventCommandValidator(IEventRepository eventRepository, IImagesService imagesService, IRoomsService roomsService)
    {
        RuleFor(x => x.Id).Must(eventRepository.IsExists).WithMessage("Activity does not exists.");
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.RoomId).NotEmpty();
        RuleFor(x => x.RoomId).Must(roomsService.IsRoomExists).WithMessage("Room does not exists.");
        RuleFor(x => x.PreviewImageId).Must(imagesService.IsImageExists).WithMessage("Image does not exists.");
    }
}
