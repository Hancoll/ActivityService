using MediatR;

namespace EventsService.Features.Events.GetEvents;

public record GetEventsQuery(
    DateTime? StartDateTime,
    DateTime? EndDateTime,
    int? From,
    int? Size) : IRequest<IEnumerable<Event>>;
