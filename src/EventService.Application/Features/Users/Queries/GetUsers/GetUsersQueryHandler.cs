using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Users.Queries.GetUsers;

internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly IRepository<User> _userRepository;

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetEntities();

        return users;
    }

    public GetUsersQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
}
