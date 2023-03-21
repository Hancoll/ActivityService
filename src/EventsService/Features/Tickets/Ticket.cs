using MongoDB.Bson.Serialization.Attributes;

namespace EventsService.Features.Tickets;

public class Ticket
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? Owner { get; set; } = null!;

    public int? Place { get; set; }
}
