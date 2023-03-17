using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using EventService.Domain.Specifications;
using MediatR;

namespace EventService.Application.Features.Events.Queries.GetEvents;

internal class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<Event>>
{
    private readonly IRepository<Event> _activityRepository;

    public async Task<IEnumerable<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var dateSpecification = new EventInDateRangeSpecification(request.StartDateTime, request.EndDateTime);

        var result = _activityRepository.GetEntities(dateSpecification, request.From, request.Size);

        return result;
    }

    public GetEventsQueryHandler(IRepository<Event> activityRepository)
    {
        _activityRepository = activityRepository;
    }
}
