using EventsService.Features.Events;
using EventsService.Infrastructure.RabbitMq;
using EventsService.Services.Images;
using EventsService.Services.Spaces;
using EventsService.Services.Users;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventsService;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class StubController : ControllerBase
{
    private readonly IImagesService _imagesService;
    private readonly ISpacesService _spacesService;
    private readonly IUsersService _usersService;
    private readonly IRabbitMqService _rabbitMqService;
    
    /// <summary>
    /// Получить Id существующего изображения
    /// </summary>
    [HttpGet("images")]
    public ScResult<Guid> GetImageId()
    {
        var result = _imagesService.GetRandomImageId();
        return new ScResult<Guid>(result.Result);
    }

    /// <summary>
    /// Получить Id существующего пространства
    /// </summary>
    [HttpGet("spaces")]
    public ScResult<Guid> GetSpaceId()
    {
        var result = _spacesService.GetRandomSpaceId();
        return new ScResult<Guid>(result.Result);
    }

    /// <summary>
    /// Получить Id существующего пользователя
    /// </summary>
    [HttpGet("users")]
    public ScResult<User> GetUser()
    {
        var result = _usersService.GetRandomUser();
        return new ScResult<User>(result);
    }

    /// <summary>
    /// Поместить в очередь RabbitMq ImageDeleteEvent
    /// </summary>
    [HttpPost("imageDeleteEvents")]
    public ScResult CreateImageDeleteEvent(Guid imageId)
    {
        var imageDeleteEvent = new ImageDeleteEvent(imageId);
        _rabbitMqService.SendMessage(imageDeleteEvent);

        return new ScResult();
    }

    /// <summary>
    /// Поместить в очередь RabbitMq SpaceDeleteEvent
    /// </summary>
    [HttpPost("spaceDeleteEvents")]
    public ScResult CreateSpaceDeleteEvent(Guid spaceId)
    {
        var spaceDeleteEvent = new SpaceDeleteEvent(spaceId);
        _rabbitMqService.SendMessage(spaceDeleteEvent);

        return new ScResult();
    }

    public StubController(IImagesService imagesService, ISpacesService spacesService, IUsersService usersService, IRabbitMqService rabbitMqService)
    {
        _imagesService = imagesService;
        _spacesService = spacesService;
        _usersService = usersService;
        _rabbitMqService = rabbitMqService;
    }
}

