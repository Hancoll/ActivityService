namespace EventsService.Services.Images;

public interface IImagesService
{
    Task<Guid> GetRandomImageId();

    Task<bool> IsImageExists(Guid id);
}
