namespace EventsService.Services.Users;

public interface IUsersService
{
    User GetRandomUser();

    bool IsExists(Guid id);
}
