using ActivityService.Domain.ActivityAggregate;
using MediatR;

namespace ActivityService.Application.Features.Activities.Queries.SearchActivities;

public record SearchActivitiesQuery(
    DateTime? StartDateTime,
    DateTime? EndDateTime,
    int? From,
    int? Size) : IRequest<IEnumerable<Activity>>;
