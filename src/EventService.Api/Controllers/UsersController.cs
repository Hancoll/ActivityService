using EventService.Application.Features.Users.Commands.AddUser;
using EventService.Application.Features.Users.Queries.GetUsers;
using EventService.Contracts.Users.Requests;
using EventService.Contracts.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Api.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Добавить пользователя
    /// </summary>
    [HttpPost]
    public async Task<ScResult<UserResponse>> AddUser(AddUserRequest request)
    {
        var command = new AddUserCommand(request.Nickname);
        var addUserResult = await _mediator.Send(command);

        var response = new UserResponse(addUserResult.Id, addUserResult.Nickname);

        return new(response);
    }

    /// <summary>
    /// Получить список пользователей
    /// </summary>
    [HttpGet]
    public async Task<ScResult<IEnumerable<UserResponse>>> GetUsers()
    {
        var query = new GetUsersQuery();
        var getUsersResult = await _mediator.Send(query);

        var response = getUsersResult.Select(x => new UserResponse(x.Id, x.Nickname));

        return new(response);
    }

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
