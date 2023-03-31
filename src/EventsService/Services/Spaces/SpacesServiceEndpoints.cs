namespace EventsService.Services.Spaces;

public class SpacesServiceEndpoints
{
    public const string SectionName = "SpacesServiceEndpoints";

    public string BaseAddress { get; init; } = null!;

    public string Spaces { get; init; } = null!;

    public string SpaceExistence { get; init; } = null!;
}
