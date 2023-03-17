using FluentValidation;

namespace EventService.Application.Features.Events.Queries.GetEvents;

internal class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
{
    public GetEventsQueryValidator()
    {
        RuleFor(x => x.From).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Size).GreaterThan(0);
    }
}
