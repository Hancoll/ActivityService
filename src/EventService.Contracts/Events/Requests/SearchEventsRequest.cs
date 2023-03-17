namespace EventService.Contracts.Events.Requests;

public record SearchEventsRequest(
    DateTime? StartDateTime,
    DateTime? EndDateTime,
    int? From,
    int? Size);
