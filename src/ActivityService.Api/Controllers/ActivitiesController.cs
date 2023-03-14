using ActivityService.Application.Features.Activities.Commands.CreateActivity;
using ActivityService.Contracts.Activities.Requests;
using ActivityService.Contracts.Activities.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        [HttpPost]
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

        [HttpPut("{activityId:guid}")]
        public async Task<IActionResult> UpdateActivity(Guid activityId, ActivityRequest request)
        {
            

            //var response = new ActivityResponse();

            return Ok();
        }

        [HttpDelete("{activityId:guid}")]
        public async Task<IActionResult> DeleteActivity(Guid activityId)
        {


            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchActivities([FromQuery] SearchActivitiesRequest request)
        {


            return Ok();
        }

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
