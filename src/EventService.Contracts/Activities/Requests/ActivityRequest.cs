namespace ActivityService.Contracts.Activities.Requests;

public record ActivityRequest(
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Name,
    string Description,
    Guid PreviewImageId,
    Guid RoomId);
