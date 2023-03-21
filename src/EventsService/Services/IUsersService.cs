namespace EventsService.Services;

public interface IUsersService
{
    User GetRandomUser();

    bool IsExists(Guid id);
}
