using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace EventsService.Services.Images;

public class ImagesService : IImagesService
{
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _asyncRetryPolicy;

    public ImagesService(HttpClient httpClient, IOptions<ServiceEndpoints> serviceEndpoints)
    {
        _httpClient = httpClient;
        var address = serviceEndpoints.Value.ImagesService;
        _httpClient.BaseAddress = new Uri(address);

        _asyncRetryPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>()
            .RetryAsync(retryCount: 3);
    }

    public async Task<Guid> GetRandomImageId() =>
        await _httpClient.GetFromJsonAsync<Guid>("GetRandomImageId");

    public async Task<bool> IsImageExists(Guid id)
    {
        var httpResponse = await _asyncRetryPolicy.ExecuteAsync(async () =>
        {
            var httpResponse = await _httpClient.GetAsync($"IsImageExists?imageId={id}");
            httpResponse.EnsureSuccessStatusCode();
            return httpResponse;
        });

        return await _httpClient.GetFromJsonAsync<bool>($"IsImageExists?imageId={id}");
    }
}
