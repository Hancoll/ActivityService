using EventService.Application.Features.Events.Commands.AddEvent;
using EventService.Application.Features.Events.Commands.DeleteEvent;
using EventService.Application.Features.Events.Commands.UpdateEvent;
using EventService.Application.Features.Events.Queries.GetEvents;
using EventService.Contracts.Events.Requests;
using EventService.Contracts.Events.Responses;
using EventService.Contracts.Tickets.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Api.Controllers;

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
    [ProducesResponseType(typeof(EventResponse), 200)]
    public async Task<ScResult<EventResponse>> AddEvent(EventRequest request)
    {
        var command = new AddEventCommand(request.StartDateTime, request.EndDateTime, request.Name,
            request.Description, request.PreviewImageId, request.RoomId);
        var addEventResult = await _mediator.Send(command);

        var response = new EventResponse(
            addEventResult.Id,
            addEventResult.StartDateTime,
            addEventResult.EndDateTime,
            addEventResult.Name,
            addEventResult.Description,
            addEventResult.PreviewImageId,
            addEventResult.RoomId,
            addEventResult.Tickets.Select(ticket => new TicketResponse(ticket.Id, ticket.Owner)),
            addEventResult.HasFreePlaces);

        return new (response);
    }

    /// <summary>
    /// Изменить мероприятие
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [ProducesResponseType(typeof(EventResponse), 200)]
    [HttpPut("{eventId:guid}")]
    public async Task<ScResult<EventResponse>> UpdateEvent(Guid eventId, EventRequest request)
    {
        var command = new UpdateEventCommand(
            eventId,
            request.StartDateTime,
            request.EndDateTime,
            request.Name,
            request.Description,
            request.PreviewImageId,
            request.RoomId);
        var updateEventResult = await _mediator.Send(command);

        var response = new EventResponse(
            updateEventResult.Id,
            updateEventResult.StartDateTime,
            updateEventResult.EndDateTime,
            updateEventResult.Name,
            updateEventResult.Description,
            updateEventResult.PreviewImageId,
            updateEventResult.RoomId,
            updateEventResult.Tickets.Select(ticket => new TicketResponse(ticket.Id, ticket.Owner)),
            updateEventResult.HasFreePlaces);

        return new (response);
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

        return new ();
    }

    /// <summary>
    /// Получить список мероприятий
    /// </summary>
    /// <response code="200">Success</response> 
    [ProducesResponseType(typeof(IEnumerable<EventResponse>), 200)]
    [HttpGet]
    public async Task<ScResult<IEnumerable<EventResponse>>> GetEvents([FromQuery] SearchEventsRequest request)
    {
        var query = new GetEventsQuery(request.StartDateTime, request.EndDateTime, request.From, request.Size);
        var getEventsResult = await _mediator.Send(query);

        var response = getEventsResult.
            Select(@event => new EventResponse(
                @event.Id,
                @event.StartDateTime,
                @event.EndDateTime,
                @event.Name,
                @event.Description,
                @event.PreviewImageId,
                @event.RoomId,
                @event.Tickets.Select(ticket => new TicketResponse(ticket.Id, ticket.Owner)),
                @event.HasFreePlaces));

        return new (response);
    }

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
