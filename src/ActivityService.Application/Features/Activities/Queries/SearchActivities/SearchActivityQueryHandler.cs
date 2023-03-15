using ActivityService.Application.Common.Interfaces;
using ActivityService.Domain.ActivityAggregate;
using ActivityService.Domain.ActivityAggregate.Specifications;
using MediatR;

namespace ActivityService.Application.Features.Activities.Queries.SearchActivities;

internal class SearchActivityQueryHandler : IRequestHandler<SearchActivitiesQuery, IEnumerable<Activity>>
{
    private readonly IRepository<Activity> _activityReposioty;

    public async Task<IEnumerable<Activity>> Handle(SearchActivitiesQuery request, CancellationToken cancellationToken)
    {
        ActivityInDateRangeSpecification? dateSpecification = null;

        if (request.StartDateTime is not null && request.EndDateTime is not null)
            dateSpecification = new ActivityInDateRangeSpecification((DateTime)request.StartDateTime, (DateTime)request.EndDateTime);

        var result = _activityReposioty.GetEntities(dateSpecification, request.From, request.Size);

        return result;
    }

    public SearchActivityQueryHandler(IRepository<Activity> activityReposioty)
    {
        _activityReposioty = activityReposioty;
    }
}
