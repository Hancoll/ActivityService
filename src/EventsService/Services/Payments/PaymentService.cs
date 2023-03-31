using Microsoft.Extensions.Options;
using SC.Internship.Common.ScResult;

namespace EventsService.Services.Payments;

public class PaymentService : IPaymentsService
{
    private readonly HttpClient _httpClient;
    private readonly PaymentsServiceEndpoints _endpoints;

    public PaymentService(HttpClient httpClient, IOptions<PaymentsServiceEndpoints> options)
    {
        _httpClient = httpClient;
        _endpoints = options.Value;

        _httpClient.BaseAddress = new Uri(_endpoints.BaseAddress);
    }

    public async Task<Guid> CreatePaymentOperation()
    {
        var result = await _httpClient.PostAsync(_endpoints.Payments, null);
        var paymentId = await result.Content.ReadFromJsonAsync<ScResult<Guid>>();

        return paymentId!.Result;
    }

    public async Task ConfirmPaymentOperation(Guid paymentId)
    {
        var path = $"{_endpoints.Confirmation}/{paymentId}";
        await _httpClient.PostAsync(path, null);
    }

    public async Task CancelPaymentOperation(Guid paymentId)
    {
        var path = $"{_endpoints.Cancellation}/{paymentId}";
        await _httpClient.PostAsync(path, null);
    }
}
