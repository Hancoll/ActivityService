using EventsService.Features.Events;
using MongoDB.Driver;

namespace EventsService.Infrastructure.Persistence;

public interface IApplicationContext
{
    IMongoCollection<Event> Events { get; }
}
