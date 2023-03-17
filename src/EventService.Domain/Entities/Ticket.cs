using EventService.Domain.Common;

namespace EventService.Domain.Entities;

public class Ticket : Entity
{
    public Guid? Owner { get; set; } = null!;

    public Event Event { get; set; }

    public Ticket(Guid id, Event @event) : base(id)
    {
        Event = @event;
    }
}
