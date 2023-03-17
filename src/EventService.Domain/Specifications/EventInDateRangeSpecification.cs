using EventService.Domain.Common;
using EventService.Domain.Entities;
using System.Linq.Expressions;

namespace EventService.Domain.Specifications;

public class EventInDateRangeSpecification : Specification<Event>
{
    private readonly DateTime _startDateTime = DateTime.MinValue;
    private readonly DateTime _endDateTime = DateTime.MaxValue;

    public override Expression<Func<Event, bool>> ToExpression()
    {
        return activity =>
            activity.StartDateTime >= _startDateTime &&
            activity.StartDateTime <= _endDateTime;
    }

    public EventInDateRangeSpecification(DateTime? startDateTime, DateTime? endDateTime)
    {
        if(startDateTime is not null)
            _startDateTime = (DateTime)startDateTime;
        if(endDateTime is not null)
            _endDateTime = (DateTime)endDateTime;
    }
}
