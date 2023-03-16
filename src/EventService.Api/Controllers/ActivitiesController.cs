using ActivityService.Application.Features.Activities.Commands.CreateActivity;
using ActivityService.Application.Features.Activities.Commands.DeleteActivity;
using ActivityService.Application.Features.Activities.Commands.UpdateActivity;
using ActivityService.Application.Features.Activities.Queries.SearchActivities;
using ActivityService.Contracts.Activities.Requests;
using ActivityService.Contracts.Activities.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ActivityService.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Add activity
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [HttpPost]
    [ProducesResponseType(typeof(ActivityResponse), 200)]
    public async Task<IActionResult> AddActivity(ActivityRequest request)
    {
        var command = new CreateActivityCommand(request.StartDateTime, request.EndDateTime, request.Name,
            request.Description, request.PreviewImageId, request.RoomId);
        var createActivityResult = await _mediator.Send(command);

        var response = new ActivityResponse(
            createActivityResult.Id,
            createActivityResult.StartDateTime,
            createActivityResult.EndDateTime,
            createActivityResult.Name,
            createActivityResult.Description,
            createActivityResult.PreviewImageId,
            createActivityResult.RoomId);

        return Ok(response);
    }

    /// <summary>
    /// Update activity
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Validation fault</response>
    [ProducesResponseType(typeof(ActivityResponse), 200)]
    [HttpPut("{activityId:guid}")]
    public async Task<IActionResult> UpdateActivity(Guid activityId, ActivityRequest request)
    {
        var command = new UpdateActivityCommand(
            activityId,
            request.StartDateTime,
            request.EndDateTime,
            request.Name,
            request.Description,
            request.PreviewImageId,
            request.RoomId);
        var updateActivityResult = await _mediator.Send(command);

        var response = new ActivityResponse(
            updateActivityResult.Id,
            updateActivityResult.StartDateTime,
            updateActivityResult.EndDateTime,
            updateActivityResult.Name,
            updateActivityResult.Description,
            updateActivityResult.PreviewImageId,
            updateActivityResult.RoomId);

        return Ok(response);
    }

    /// <summary>
    /// Delete activity
    /// </summary>
    /// <response code="200">Success</response> 
    [HttpDelete("{activityId:guid}")]
    public async Task<IActionResult> DeleteActivity(Guid activityId)
    {
        var command = new DeleteActivityCommand(activityId);
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Get list of activities
    /// </summary>
    /// <response code="200">Success</response> 
    [ProducesResponseType(typeof(IEnumerable<ActivityResponse>), 200)]
    [HttpGet("search")]
    public async Task<IActionResult> SearchActivities([FromQuery] SearchActivitiesRequest request)
    {
        var query = new SearchActivitiesQuery(request.StartDateTime, request.EndDateTime, request.From, request.Size);
        var searchActivitiesResult = await _mediator.Send(query);

        var response = searchActivitiesResult.
            Select(activity => new ActivityResponse(
                activity.Id,
                activity.StartDateTime,
                activity.EndDateTime,
                activity.Name,
                activity.Description,
                activity.PreviewImageId,
                activity.RoomId));

        return Ok(searchActivitiesResult);
    }

    public ActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
