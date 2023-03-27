using EventsService.Features.Events.AddEvent;
using EventsService.Features.Events.DeleteEvent;
using EventsService.Features.Events.GetEvents;
using EventsService.Features.Events.UpdateEvent;
using EventsService.Features.Tickets;
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
    private readonly ILogger<EventsController> _logger;

    public EventsController(IMediator mediator, ILogger<EventsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Добавить мероприятие
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [HttpPost]
    [ProducesResponseType(typeof(Event), 200)]
    public async Task<ScResult<Event>> AddEvent(EventRequest request)
    {
        var command = new AddEventCommand(request.StartDateTime, request.EndDateTime, request.Name,
            request.Description, request.PreviewImageId, request.RoomId, request.HasPlaces, request.Price);
        var addEventResult = await _mediator.Send(command);

        var response = new Event(
            addEventResult.Id,
            addEventResult.StartDateTime,
            addEventResult.EndDateTime,
            addEventResult.Name,
            addEventResult.Description,
            addEventResult.PreviewImageId,
            addEventResult.SpaceId,
            addEventResult.Tickets.Select(ticket => new Ticket { Id = ticket.Id, Owner = ticket.Owner, Place = ticket.Place }).ToList(),
            addEventResult.HasPlaces,
            addEventResult.Price);

        _logger.LogInformation($"Event {response.Id} added");

        return new ScResult<Event>(response);
    }

    /// <summary>
    /// Изменить мероприятие
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [ProducesResponseType(typeof(Event), 200)]
    [HttpPut("{eventId:guid}")]
    public async Task<ScResult<Event>> UpdateEvent(Guid eventId, EventRequest request)
    {
        var command = new UpdateEventCommand(
            eventId,
            request.StartDateTime,
            request.EndDateTime,
            request.Name,
            request.Description,
            request.PreviewImageId,
            request.RoomId,
            request.Tickets,
            request.HasPlaces,    
            request.Price);
        var updateEventResult = await _mediator.Send(command);

        var response = new Event(
            updateEventResult.Id,
            updateEventResult.StartDateTime,
            updateEventResult.EndDateTime,
            updateEventResult.Name,
            updateEventResult.Description,
            updateEventResult.PreviewImageId,
            updateEventResult.SpaceId,
            updateEventResult.Tickets.Select(ticket => new Ticket { Id = ticket.Id, Owner = ticket.Owner, Place = ticket.Place }).ToList(),
            updateEventResult.HasPlaces,
            updateEventResult.Price);

        _logger.LogInformation($"Event {response.Id} updated");

        return new ScResult<Event>(response);
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

        _logger.LogInformation($"Event {eventId} deleted");

        return new ScResult();
    }

    /// <summary>
    /// Получить список мероприятий
    /// </summary>
    /// <response code="200">Success</response> 
    [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
    [HttpGet]
    public async Task<ScResult<IEnumerable<Event>>> GetEvents([FromQuery] GetEventsRequest request)
    {
        var query = new GetEventsQuery(request.StartDateTime, request.EndDateTime, request.From, request.Size);
        var getEventsResult = await _mediator.Send(query);

        var response = getEventsResult.
            Select(@event => new Event(
                @event.Id,
                @event.StartDateTime,
                @event.EndDateTime,
                @event.Name,
                @event.Description,
                @event.PreviewImageId,
                @event.SpaceId,
                @event.Tickets.Select(ticket => new Ticket { Id = ticket.Id, Owner = ticket.Owner, Place = ticket.Place }).ToList(),
                @event.HasPlaces,
                @event.Price));

        return new ScResult<IEnumerable<Event>>(response);
    }
}
