namespace EventsService.Services;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Nickname { get; set; }

    public User(string nickname)
    {
        Nickname = nickname;
    }
}
