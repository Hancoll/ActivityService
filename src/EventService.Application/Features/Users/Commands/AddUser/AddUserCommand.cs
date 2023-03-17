using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Users.Commands.AddUser;

public record AddUserCommand(
    string Nickname) : IRequest<User>;
