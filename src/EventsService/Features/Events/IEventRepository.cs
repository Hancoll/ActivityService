namespace EventsService.Features.Events;

public interface IEventRepository
{
    Event GetEvent(Guid id);

    IEnumerable<Event> GetEvents(DateTime? start, DateTime? end, int? from, int? size);

    void Add(Event @event);

    void Delete(Guid id);

    void Update(Event @event);

    bool IsExists(Guid id);
}
