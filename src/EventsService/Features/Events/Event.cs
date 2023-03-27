using EventsService.Features.Tickets;
using MongoDB.Bson.Serialization.Attributes;

namespace EventsService.Features.Events;

public class Event
{
    [BsonId]
    public Guid Id { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? PreviewImageId { get; set; }

    public Guid SpaceId { get; set; }

    /// <summary>
    /// Есть ли у билетов на мероприятие места
    /// </summary>
    public bool HasPlaces { get; set; }

    public List<Ticket> Tickets { get; set; }

    public decimal? Price { get; set; }

    public Event(Guid id, DateTime startDateTime, DateTime endDateTime, string name, string description, Guid? previewImageId, Guid spaceId, List<Ticket> tickets, bool hasPlaces, decimal? price)
    {
        Id = id;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Name = name;
        Description = description;
        PreviewImageId = previewImageId;
        SpaceId = spaceId;
        Tickets = tickets;
        HasPlaces = hasPlaces;
        Price = price;
    }
}

