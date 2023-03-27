using EventsService.Features.Events;
using FluentValidation;
using JetBrains.Annotations;

namespace EventsService.Features.Tickets.SellTicketToUser;

[UsedImplicitly]
public class SellTicketToUserCommandValidator : AbstractValidator<SellTicketToUserCommand>
{
    public SellTicketToUserCommandValidator(IEventRepository eventRepository)
    {
        RuleFor(x => x.Place)
            .Must(x => x is not null)
            .GreaterThan(0)
            .When(x => eventRepository.GetEvent(x.EventId).HasPlaces)
            .WithMessage("Место не указано.");
    }
}
