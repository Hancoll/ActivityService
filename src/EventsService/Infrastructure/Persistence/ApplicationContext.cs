using EventsService.Features.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EventsService.Infrastructure.Persistence;

public class ApplicationContext : IApplicationContext
{
    private const string EventsCollectionName = "Events";

    public IMongoCollection<Event> Events { get; }

    public ApplicationContext(IOptions<DatabaseSettings> databaseSettings)
    {
        var client = new MongoClient(databaseSettings.Value.ConnectionString);
        var db = client.GetDatabase(databaseSettings.Value.DatabaseName);

        Events = db.GetCollection<Event>(EventsCollectionName);
    }
}
