namespace ActivityService.Application.Features.Rooms;

public class RoomsService
{
    private readonly List<Guid> _rooms = new List<Guid>() { Guid.NewGuid() };

    public Guid GetRandomRoomId()
    {
        return _rooms.First();
    }

    public bool IsRoomExists(Guid guid)
    {
        return _rooms.Contains(guid);
    }
}
