using ActivityService.Domain.Common;
using ActivityService.Domain.Entities;
using System.Linq.Expressions;

namespace ActivityService.Domain.Specifications;

public class ActivityInDateRangeSpecification : Specification<Activity>
{
    private readonly DateTime _startDateTime = DateTime.MinValue;
    private readonly DateTime _endDateTime = DateTime.MaxValue;

    public override Expression<Func<Activity, bool>> ToExpression()
    {
        return activity =>
            activity.StartDateTime >= _startDateTime &&
            activity.StartDateTime <= _endDateTime;
    }

    public ActivityInDateRangeSpecification(DateTime? startDateTime, DateTime? endDateTime)
    {
        if(startDateTime is not null)
            _startDateTime = (DateTime)startDateTime;
        if(endDateTime is not null)
            _endDateTime = (DateTime)endDateTime;
    }
}
