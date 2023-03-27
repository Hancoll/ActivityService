using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Events.GetEvents;

[UsedImplicitly]
public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<Event>>
{
    private readonly IEventRepository _eventRepository;

    public Task<IEnumerable<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var result = _eventRepository.GetEvents(request.StartDateTime, request.EndDateTime, request.From, request.Size);

        return Task.FromResult(result);
    }

    public GetEventsQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
