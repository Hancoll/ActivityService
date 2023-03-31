using AutoMapper;
using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Events.UpdateEvent;

[UsedImplicitly]
public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Event>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public Task<Event> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
    {
        var @event = _mapper.Map<Event>(command);
        _eventRepository.Update(@event);

        return Task.FromResult(@event);
    }

    public UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }
}
