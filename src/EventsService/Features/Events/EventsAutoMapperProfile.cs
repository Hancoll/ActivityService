using AutoMapper;
using EventsService.Features.Events.AddEvent;
using EventsService.Features.Events.GetEvents;
using EventsService.Features.Events.UpdateEvent;
using JetBrains.Annotations;

namespace EventsService.Features.Events;

[UsedImplicitly]
public class EventsAutoMapperProfile : Profile
{
    public EventsAutoMapperProfile()
    {
        CreateMap<AddEventRequest, AddEventCommand>();
        CreateMap<AddEventCommand, Event>();
        CreateMap<UpdateEventRequest, UpdateEventCommand>();
        CreateMap<UpdateEventCommand, Event>();
        CreateMap<GetEventsRequest, GetEventsQuery>();
    }
}
