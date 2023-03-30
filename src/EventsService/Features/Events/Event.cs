using EventsService.Features.Tickets;
using MongoDB.Bson.Serialization.Attributes;

namespace EventsService.Features.Events;

public class Event
{
    [BsonId]
    public Guid Id { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid? PreviewImageId { get; set; }

    public Guid SpaceId { get; set; }

    /// <summary>
    /// Есть ли у билетов на мероприятие места
    /// </summary>
    public bool HasPlaces { get; set; }

    public List<Ticket> Tickets { get; set; } = new();

    public decimal? Price { get; set; }
}

