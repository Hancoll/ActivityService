using EventsService.Features.Tickets;
using MongoDB.Bson.Serialization.Attributes;

namespace EventsService.Features.Events;

public class Event
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid PreviewImageId { get; set; }

    public Guid RoomId { get; set; }

    /// <summary>
    /// Есть ли у билетов на мероприятие места
    /// </summary>
    public bool HasPlaces { get; set; }

    public List<Ticket> Tickets { get; set; } = new();

    public Event(DateTime startDateTime, DateTime endDateTime, string name, string description, Guid previewImageId, Guid roomId, bool hasPlaces)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Name = name;
        Description = description;
        PreviewImageId = previewImageId;
        RoomId = roomId;
        HasPlaces = hasPlaces;
    }
}

