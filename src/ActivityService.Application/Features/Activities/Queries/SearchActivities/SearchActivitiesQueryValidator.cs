using FluentValidation;

namespace ActivityService.Application.Features.Activities.Queries.SearchActivities;

internal class SearchActivitiesQueryValidator : AbstractValidator<SearchActivitiesQuery>
{
    public SearchActivitiesQueryValidator()
    {
        RuleFor(x => x.From).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Size).GreaterThan(0);
    }
}
