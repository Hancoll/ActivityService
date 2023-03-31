using EventsService.Services.Images;
using EventsService.Services.Spaces;
using FluentValidation;
using JetBrains.Annotations;

namespace EventsService.Features.Events.UpdateEvent;

[UsedImplicitly]
public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventCommandValidator(IEventRepository eventRepository, IImagesService imagesService, ISpacesService spacesService)
    {
        RuleFor(x => x.Id).Must(eventRepository.IsExists).WithMessage("Activity does not exists.");
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.SpaceId).NotEmpty();
        RuleFor(x => x.SpaceId).Must(x => spacesService.IsSpaceExists(x).Result)
            .WithMessage("Room does not exists.");
        RuleFor(x => x.PreviewImageId)
            .Must(x => imagesService.IsImageExists((Guid)x!).Result)
            .When(x => x.PreviewImageId is not null)
            .WithMessage("Image does not exists.");
    }
}
