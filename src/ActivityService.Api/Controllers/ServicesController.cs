using ActivityService.Application.Features.Images;
using ActivityService.Application.Features.Rooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityService.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
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

    public ServicesController(ImagesService imagesService, RoomsService roomsService)
    {
        _imagesService = imagesService;
        _roomsService = roomsService;
    }
}
