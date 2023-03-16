using ActivityService.Domain.Common;

namespace ActivityService.Domain.Entities;

public class Activity : Entity
{
    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid PreviewImageId { get; set; }

    public Guid RoomId { get; set; }

    public List<Ticket> Tickets { get; set; }

    public bool HasFreePlaces { get; set; }

    public Activity(Guid id, DateTime startDateTime, DateTime endDateTime, string name, string description, Guid previewImageId, Guid roomId) : base(id)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Name = name;
        Description = description;
        PreviewImageId = previewImageId;
        RoomId = roomId;
        Tickets = new List<Ticket>();
        HasFreePlaces = true;
    }
}

