namespace EventsService.Features.Events;

public record GetEventsRequest(
    DateTime? StartDateTime,
    DateTime? EndDateTime,
    int? From,
    int? Size);
