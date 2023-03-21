namespace EventsService.Services;

public interface IImagesService
{
    Guid GetRandomImageId();

    bool IsImageExists(Guid id);
}
