using EventService.Domain.Entities;
using MediatR;

namespace EventService.Application.Features.Events.Commands.AddEvent;

public record AddEventCommand(
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId) : IRequest<Event>;
