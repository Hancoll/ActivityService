using ActivityService.Domain.Common;

namespace ActivityService.Domain.Entities;

public class User : Entity
{
    public string NickName { get; set; }

    public User(Guid id, string nickName) : base(id)
    {
        NickName = nickName;
    }
}
