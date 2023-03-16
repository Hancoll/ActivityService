using ActivityService.Domain.Common;

namespace ActivityService.Domain.Entities;

public class Ticket : Entity
{
    public Guid Owner { get; set; }

    public Ticket(Guid id, Guid owner) : base(id)
    {
        Owner = owner;
    }
}
