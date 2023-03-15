using ActivityService.Domain.Common;
using System.Linq.Expressions;

namespace ActivityService.Domain.ActivityAggregate.Specifications;

public class ActivityInDateRangeSpecification : Specification<Activity>
{
    private readonly DateTime _startDateTime;
    private readonly DateTime _endDateTime;

    public override Expression<Func<Activity, bool>> ToExpression()
    {
        return activity => 
            activity.StartDateTime >= _startDateTime &&
            activity.StartDateTime <= _endDateTime;
    }

    public ActivityInDateRangeSpecification(DateTime startDateTime, DateTime endDateTime)
    {
        _startDateTime = startDateTime;
        _endDateTime = endDateTime;
    }
}
