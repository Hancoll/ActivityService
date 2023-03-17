using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<IEnumerable<User>>;
