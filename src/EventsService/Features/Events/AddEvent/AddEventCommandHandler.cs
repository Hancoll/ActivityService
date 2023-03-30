using AutoMapper;
using JetBrains.Annotations;
using MediatR;

namespace EventsService.Features.Events.AddEvent;

[UsedImplicitly]
public class AddEventCommandHandler : IRequestHandler<AddEventCommand, Event>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public Task<Event> Handle(AddEventCommand command, CancellationToken cancellationToken)
    {
        var @event = _mapper.Map<Event>(command, opt =>
        {
            opt.AfterMap((src, dest) => dest.Id = Guid.NewGuid());
        });
        _eventRepository.Add(@event);

        return Task.FromResult(@event);
    }

    public AddEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }
}
