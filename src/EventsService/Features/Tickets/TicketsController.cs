using EventsService.Features.Tickets.AddTicketsToEvent;
using EventsService.Features.Tickets.HasUserTicketToEvent;
using EventsService.Features.Tickets.IssueTicketToUser;
using EventsService.Features.Tickets.SellTicketToUser;
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
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(IMediator mediator, ILogger<TicketsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Добавить билеты для мероприятия
    /// </summary>
    [HttpPost("{eventId:guid}")]
    public async Task<ScResult> AddTickets(Guid eventId, AddTicketsRequest request)
    {
        var command = new AddTicketsToEventCommand(eventId, request.Count);
        await _mediator.Send(command);

        _logger.LogInformation($"Tickets added to event {eventId}");

        return new ScResult();
    }

    /// <summary>
    /// Выдать билет пользователю на мероприятие
    /// </summary>
    [HttpPost("users/{userId:guid}/events/{eventId:guid}/issue")]
    public async Task<ScResult<Ticket>> IssueTicket(Guid userId, Guid eventId, IssueTicketRequest request)
    {
        var command = new IssueTicketToUserCommand(userId, eventId, request.Place);
        var issueTicketResult = await _mediator.Send(command);

        var response = new Ticket 
        { 
            Id = issueTicketResult.Id,  
            Owner = issueTicketResult.Owner, 
            Place = issueTicketResult.Place 
        };

        _logger.LogInformation($"Ticket issued to user {userId} to event {eventId}");

        return new ScResult<Ticket>(response);
    }

    /// <summary>
    /// Продать билет пользователю на мероприятие
    /// </summary>
    [HttpPost("users/{userId:guid}/events/{eventId:guid}/sell")]
    public async Task<ScResult<Ticket>> SellTicket(Guid userId, Guid eventId, IssueTicketRequest request)
    {
        var command = new SellTicketToUserCommand(userId, eventId, request.Place);
        var sellTicketResult = await _mediator.Send(command);

        var response = new Ticket
        {
            Id = sellTicketResult.Id,
            Owner = sellTicketResult.Owner,
            Place = sellTicketResult.Place
        };

        _logger.LogInformation($"Ticket sold to user {userId} to event {eventId}");

        return new ScResult<Ticket>(response);
    }

    /// <summary>
    /// Проверить есть ли билет у пользователя на данное мероприятие
    /// </summary>
    [HttpGet("users/{userId:guid}/events/{eventId:guid}")]
    public async Task<ScResult<bool>> HasTicket(Guid userId, Guid eventId)
    {
        var query = new HasUserTicketToEventQuery(userId, eventId);
        var hasUserTicketToEventResult = await _mediator.Send(query);

        return new ScResult<bool>(hasUserTicketToEventResult);
    }
}
