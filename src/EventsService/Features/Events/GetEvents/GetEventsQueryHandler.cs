using MediatR;

namespace EventsService.Features.Events.GetEvents;

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<Event>>
{
    private readonly IEventRepository _eventRepository;

    public async Task<IEnumerable<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var result = _eventRepository.GetEvents(request.StartDateTime, request.EndDateTime, request.From, request.Size);

        return result;
    }

    public GetEventsQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
