using MediatR;

namespace EventService.Application.Features.Tickets.Commands.AddTicketsToEvent;

public record AddTicketsToEventCommand(
    Guid EventId,
    int Count) : IRequest;
