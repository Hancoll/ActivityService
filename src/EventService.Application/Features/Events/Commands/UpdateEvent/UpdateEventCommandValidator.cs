using EventService.Application.Common.Interfaces;
using EventService.Application.Features.Images;
using EventService.Application.Features.Rooms;
using EventService.Domain.Entities;
using FluentValidation;

namespace EventService.Application.Features.Events.Commands.UpdateEvent;

internal class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventCommandValidator(IRepository<Event> activityRepository, ImagesService imagesService, RoomsService roomsService)
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
