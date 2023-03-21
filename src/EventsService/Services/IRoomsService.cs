namespace EventsService.Services;

public interface IRoomsService
{
    Guid GetRandomRoomId();

    bool IsRoomExists(Guid id);
}
