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

    /// <summary>
    /// Добавить мероприятие
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [HttpPost]
    [ProducesResponseType(typeof(EventDto), 200)]
    public async Task<ScResult<EventDto>> AddEvent(EventRequest request)
    {
        var command = new AddEventCommand(request.StartDateTime, request.EndDateTime, request.Name,
            request.Description, request.PreviewImageId, request.RoomId, request.HasPlaces);
        var addEventResult = await _mediator.Send(command);

        var response = new EventDto(
            addEventResult.Id,
            addEventResult.StartDateTime,
            addEventResult.EndDateTime,
            addEventResult.Name,
            addEventResult.Description,
            addEventResult.PreviewImageId,
            addEventResult.RoomId,
            addEventResult.Tickets.Select(ticket => new TicketDto(ticket.Id, ticket.Owner, ticket.Place)),
            addEventResult.HasPlaces);

        return new ScResult<EventDto>(response);
    }

    /// <summary>
    /// Изменить мероприятие
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [ProducesResponseType(typeof(EventDto), 200)]
    [HttpPut("{eventId:guid}")]
    public async Task<ScResult<EventDto>> UpdateEvent(Guid eventId, EventRequest request)
    {
        var command = new UpdateEventCommand(
            eventId,
            request.StartDateTime,
            request.EndDateTime,
            request.Name,
            request.Description,
            request.PreviewImageId,
            request.RoomId,
            request.HasPlaces);
        var updateEventResult = await _mediator.Send(command);

        var response = new EventDto(
            updateEventResult.Id,
            updateEventResult.StartDateTime,
            updateEventResult.EndDateTime,
            updateEventResult.Name,
            updateEventResult.Description,
            updateEventResult.PreviewImageId,
            updateEventResult.RoomId,
            updateEventResult.Tickets.Select(ticket => new TicketDto(ticket.Id, ticket.Owner, ticket.Place)),
            updateEventResult.HasPlaces);

        return new ScResult<EventDto>(response);
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
    [ProducesResponseType(typeof(IEnumerable<EventDto>), 200)]
    [HttpGet]
    public async Task<ScResult<IEnumerable<EventDto>>> GetEvents([FromQuery] GetEventsRequest request)
    {
        var query = new GetEventsQuery(request.StartDateTime, request.EndDateTime, request.From, request.Size);
        var getEventsResult = await _mediator.Send(query);

        var response = getEventsResult.
            Select(@event => new EventDto(
                @event.Id,
                @event.StartDateTime,
                @event.EndDateTime,
                @event.Name,
                @event.Description,
                @event.PreviewImageId,
                @event.RoomId,
                @event.Tickets.Select(ticket => new TicketDto(ticket.Id, ticket.Owner, ticket.Place)),
                @event.HasPlaces));

        return new ScResult<IEnumerable<EventDto>>(response);
    }

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
