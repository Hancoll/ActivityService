namespace ActivityService.Contracts.Activities.Requests;

public record SearchActivitiesRequest(
    DateTime StartDateTime,
    DateTime EndDateTime,
    int From,
    int Size);
