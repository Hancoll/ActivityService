namespace EventsService.Services.Spaces;

public interface ISpacesService
{
    Task<Guid> GetRandomSpaceId();

    Task<bool> IsSpaceExists(Guid id);
}
