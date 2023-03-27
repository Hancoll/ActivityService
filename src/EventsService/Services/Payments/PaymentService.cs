using Microsoft.Extensions.Options;

namespace EventsService.Services.Payments;

public class PaymentService : IPaymentsService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(HttpClient httpClient, IOptions<ServiceEndpoints> serviceEndpoints, ILogger<PaymentService> logger)
    {
        _httpClient = httpClient;

        var address = serviceEndpoints.Value.PaymentsService;
        _httpClient.BaseAddress = new Uri(address);
        _logger = logger;
    }

    public async Task<Guid> CreatePaymentOperation()
    {
        var result = await _httpClient.PostAsync("create", null);
        var paymentId = await result.Content.ReadFromJsonAsync<Guid>();

        _logger.LogInformation($"Payment operation {paymentId} created");

        return paymentId;
    }

    public async Task ConfirmPaymentOperation(Guid paymentId)
    {
        await _httpClient.PostAsync("confirm/{paymentId}", null);

        _logger.LogInformation($"Payment operation {paymentId} confirmed");
    }

    public async Task CancelPaymentOperation(Guid paymentId)
    {
        await _httpClient.PostAsync($"cancel/{paymentId}", null);

        _logger.LogInformation($"Payment operation {paymentId} canceled");
    }
}
