using EventService.Application.Common.Interfaces;
using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Users.Commands.AddUser;

internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
{
    private readonly IRepository<User> _userRepository;

    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(Guid.NewGuid(), request.Nickname);

        _userRepository.Insert(user);

        return user;
    }

    public AddUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
}
