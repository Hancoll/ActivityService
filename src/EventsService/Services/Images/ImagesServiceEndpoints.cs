namespace EventsService.Services.Images;

public class ImagesServiceEndpoints
{
    public const string SectionName = "ImagesServiceEndpoints";

    public string BaseAddress { get; init; } = null!;

    public string Images { get; init; } = null!;

    public string ImageExistence { get; init; } = null!;
}
