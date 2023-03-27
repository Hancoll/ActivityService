using EventsService.Services.Images;
using EventsService.Services.Spaces;
using FluentValidation;

namespace EventsService.Features.Events.AddEvent;

public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    public AddEventCommandValidator(IImagesService imagesService, ISpacesService spacesService)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotEmpty().MinimumLength(4);
        RuleFor(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.RoomId).NotEmpty();
        RuleFor(x => x.RoomId).Must(x => spacesService.IsSpaceExists(x).Result)
            .WithMessage("Room does not exists.");
        RuleFor(x => x.PreviewImageId)
            .Must(x => imagesService.IsImageExists((Guid)x!).Result)
            .When(x => x.PreviewImageId is not null)
            .WithMessage("Image does not exists.");
    }
}
