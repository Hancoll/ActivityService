using EventsService.Services;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventsService;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class StubController : ControllerBase
{
    private readonly IImagesService _imagesService;
    private readonly IRoomsService _roomsService;
    private readonly IUsersService _usersService;
    
    /// <summary>
    /// Получить Id существующего изображения
    /// </summary>
    [HttpGet("image")]
    public ScResult<Guid> GetImageId()
    {
        var result = _imagesService.GetRandomImageId();

        return new ScResult<Guid>(result);
    }

    /// <summary>
    /// Получить Id существующего пространства
    /// </summary>
    [HttpGet("room")]
    public ScResult<Guid> GetRoomId()
    {
        var result = _roomsService.GetRandomRoomId();

        return new ScResult<Guid>(result);
    }

    [HttpGet("user")]
    public ScResult<User> GetUser()
    {
        var result = _usersService.GetRandomUser();

        return new ScResult<User>(result);
    }

    public StubController(IImagesService imagesService, IRoomsService roomsService, IUsersService usersService)
    {
        _imagesService = imagesService;
        _roomsService = roomsService;
        _usersService = usersService;
    }
}

