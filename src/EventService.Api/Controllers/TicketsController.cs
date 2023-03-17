using EventService.Application.Features.Tickets.Commands.AddTicketsToEvent;
using EventService.Application.Features.Tickets.Commands.IssueTicketToUser;
using EventService.Application.Features.Tickets.Queries.HasUserTicketToEvent;
using EventService.Contracts.Tickets.Requests;
using EventService.Contracts.Tickets.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Api.Controllers;

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

        return new();
    }

    /// <summary>
    /// Выдать билет пользователю на мероприятие
    /// </summary>
    [HttpPost("users/{userId:guid}/events/{eventId:guid}")]
    public async Task<ScResult<TicketResponse>> IssueTicket(Guid userId, Guid eventId)
    {
        var command = new IssueTicketToUserCommand(userId, eventId);
        var issueTicketResult = await _mediator.Send(command);

        var response = new TicketResponse(issueTicketResult.Id, issueTicketResult.Owner);

        return new(response);
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

        return new(response);
    }

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
