namespace ActivityService.Contracts.Activities.Responses;

public record ActivityResponse(
    Guid Id,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId);

