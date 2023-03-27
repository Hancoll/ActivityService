using MongoDB.Driver;

namespace EventsService.Features.Events;

public interface IEventRepository
{
    Event GetEvent(Guid id);

    IEnumerable<Event> GetEvents(DateTime? start, DateTime? end, int? from, int? size);

    void Add(Event @event);

    void Delete(Guid id);

    void Delete(FilterDefinition<Event> filter);

    void Update(Event @event);

    void Update(FilterDefinition<Event> filter, UpdateDefinition<Event> update);

    bool IsExists(Guid id);
}
