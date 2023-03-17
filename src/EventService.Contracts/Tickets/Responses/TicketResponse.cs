namespace EventService.Contracts.Tickets.Responses;

public record TicketResponse(
    Guid Id,
    Guid? Owner);
