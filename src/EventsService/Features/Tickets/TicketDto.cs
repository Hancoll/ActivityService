namespace EventsService.Features.Tickets;

public record TicketDto(
    Guid Id,
    Guid? Owner,
    int? Place);

