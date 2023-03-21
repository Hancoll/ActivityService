using EventsService.Features.Tickets.AddTicketsToEvent;
using EventsService.Features.Tickets.HasUserTicketToEvent;
using EventsService.Features.Tickets.IssueTicketToUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventsService.Features.Tickets;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Добавить билеты для мероприятия
    /// </summary>
    [HttpPost("{eventId:guid}")]
    public async Task<ScResult> AddTickets(Guid eventId, AddTicketsRequest request)
    {
        var command = new AddTicketsToEventCommand(eventId, request.Count);
        await _mediator.Send(command);

        return new ScResult();
    }

    /// <summary>
    /// Выдать билет пользователю на мероприятие
    /// </summary>
    [HttpPost("users/{userId:guid}/events/{eventId:guid}")]
    public async Task<ScResult<TicketDto>> IssueTicket(Guid userId, Guid eventId, IssueTicketRequest request)
    {
        var command = new IssueTicketToUserCommand(userId, eventId, request.Place);
        var issueTicketResult = await _mediator.Send(command);

        var response = new TicketDto(issueTicketResult.Id, issueTicketResult.Owner, issueTicketResult.Place);

        return new ScResult<TicketDto>(response);
    }

    /// <summary>
    /// Проверить есть ли билет у пользователя на данное мероприятие
    /// </summary>
    [HttpGet("users/{userId:guid}/events/{eventId:guid}")]
    public async Task<ScResult<bool>> HasTicket(Guid userId, Guid eventId)
    {
        var query = new HasUserTicketToEventQuery(userId, eventId);
        var hasUserTicketToEventResult = await _mediator.Send(query);

        var response = hasUserTicketToEventResult;

        return new ScResult<bool>(response);
    }

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
