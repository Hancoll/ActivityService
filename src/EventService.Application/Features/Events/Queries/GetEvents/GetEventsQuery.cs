using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Events.Queries.GetEvents;

public record GetEventsQuery(
    DateTime? StartDateTime,
    DateTime? EndDateTime,
    int? From,
    int? Size) : IRequest<IEnumerable<Event>>;
