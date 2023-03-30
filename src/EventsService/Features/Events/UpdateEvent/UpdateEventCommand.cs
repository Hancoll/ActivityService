using EventsService.Features.Tickets;
using MediatR;

namespace EventsService.Features.Events.UpdateEvent;

public class UpdateEventCommand : IRequest<Event>
{
    public Guid Id { get; set; }

    public DateTime StartDateTime { get; set; } 

    public DateTime EndDateTime { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid? PreviewImageId { get; set; }

    public Guid SpaceId { get; set; }

    public List<Ticket> Tickets { get; set; } = null!;

    public bool HasPlaces { get; set; }

    public decimal? Price { get; set; }
}
