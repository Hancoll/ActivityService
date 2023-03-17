namespace EventService.Contracts.Events.Requests;

public record EventRequest(
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId);
