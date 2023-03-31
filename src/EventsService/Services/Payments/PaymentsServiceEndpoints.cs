namespace EventsService.Services.Payments;

public class PaymentsServiceEndpoints
{
    public const string SectionName = "PaymentsServiceEndpoints";

    public string BaseAddress { get; init; } = null!;

    public string Payments { get; init; } = null!;

    public string Confirmation { get; init; } = null!;

    public string Cancellation {get; init; } = null!;
}
