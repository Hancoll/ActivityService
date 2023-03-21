using FluentValidation;
using JetBrains.Annotations;

namespace EventsService.Features.Events.GetEvents;

[UsedImplicitly]
public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
{
    public GetEventsQueryValidator()
    {
        RuleFor(x => x.From).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Size).GreaterThan(0);
    }
}
