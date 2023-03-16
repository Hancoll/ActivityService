using ActivityService.Application.Features.Images;
using ActivityService.Application.Features.Rooms;
using ActivityService.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityService.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class StubsController : ControllerBase
{
    private readonly ImagesService _imagesService;
    private readonly RoomsService _roomsService;

    /// <summary>
    /// Get id of some exist image
    /// </summary>
    [HttpGet("image")]
    public IActionResult GetImageId()
    {
        var result = _imagesService.GetRandomImageId();

        return Ok(result);
    }

    /// <summary>
    /// Get id of some exist room
    /// </summary>
    [HttpGet("room")]
    public IActionResult GetRoomId()
    {
        var result = _roomsService.GetRandomRoomId();

        return Ok(result);
    }

    [HttpGet("/stub/authstub")]
    [Authorize]
    public IActionResult AuthStub()
    {
        return Ok();
    }

    public StubsController(ImagesService imagesService, RoomsService roomsService)
    {
        _imagesService = imagesService;
        _roomsService = roomsService;
    }
}
