using AutoMapper;
using EventsService.Features.Events.AddEvent;
using EventsService.Features.Events.DeleteEvent;
using EventsService.Features.Events.GetEvents;
using EventsService.Features.Events.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventsService.Features.Events;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EventsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Добавить мероприятие
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [HttpPost("")]
    [ProducesResponseType(typeof(Event), 200)]
    public async Task<ScResult<Event>> AddEvent(AddEventRequest request)
    {
        var command = _mapper.Map<AddEventCommand>(request);
        var addEventResult = await _mediator.Send(command);

        return new ScResult<Event>(addEventResult);
    }

    /// <summary>
    /// Изменить мероприятие
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [ProducesResponseType(typeof(Event), 200)]
    [HttpPut("{eventId:guid}")]
    public async Task<ScResult<Event>> UpdateEvent(Guid eventId, UpdateEventRequest request)
    {
        var command = _mapper.Map<UpdateEventCommand>(request, opt =>
        {
            opt.AfterMap((_, dest) => dest.Id = eventId);
        });
        var updateEventResult = await _mediator.Send(command);

        return new ScResult<Event>(updateEventResult);
    }

    /// <summary>
    /// Удалить мероприятие
    /// </summary>
    /// <response code="200">Success</response> 
    [HttpDelete("{eventId:guid}")]
    public async Task<ScResult> DeleteEvent(Guid eventId)
    {
        var command = new DeleteEventCommand(eventId);
        await _mediator.Send(command);

        return new ScResult();
    }

    /// <summary>
    /// Получить список мероприятий
    /// </summary>
    /// <response code="200">Success</response> 
    [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
    [HttpGet("")]
    public async Task<ScResult<IEnumerable<Event>>> GetEvents([FromQuery] GetEventsRequest request)
    {
        var query = _mapper.Map<GetEventsQuery>(request);
        var getEventsResult = await _mediator.Send(query);

        return new ScResult<IEnumerable<Event>>(getEventsResult);
    }
}
