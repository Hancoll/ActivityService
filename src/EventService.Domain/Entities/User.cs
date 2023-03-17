using EventService.Domain.Common;

namespace EventService.Domain.Entities;

public class User : Entity
{
    public string Nickname { get; set; }

    public User(Guid id, string nickname) : base(id)
    {
        Nickname = nickname;
    }
}
