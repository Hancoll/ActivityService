using ActivityService.Application.Common.Interfaces;
using ActivityService.Domain.Entities;
using ActivityService.Domain.Specifications;
using MediatR;

namespace ActivityService.Application.Features.Activities.Queries.SearchActivities;

internal class SearchActivityQueryHandler : IRequestHandler<SearchActivitiesQuery, IEnumerable<Activity>>
{
    private readonly IRepository<Activity> _activityReposioty;

    public async Task<IEnumerable<Activity>> Handle(SearchActivitiesQuery request, CancellationToken cancellationToken)
    {
        var dateSpecification = new ActivityInDateRangeSpecification(request.StartDateTime, request.EndDateTime);

        var result = _activityReposioty.GetEntities(dateSpecification, request.From, request.Size);

        return result;
    }

    public SearchActivityQueryHandler(IRepository<Activity> activityReposioty)
    {
        _activityReposioty = activityReposioty;
    }
}
