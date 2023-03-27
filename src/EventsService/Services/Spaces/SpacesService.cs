using Microsoft.Extensions.Options;

namespace EventsService.Services.Spaces;

public class SpacesService : ISpacesService
{
    private readonly HttpClient _httpClient;

    public SpacesService(HttpClient httpClient, IOptions<ServiceEndpoints> serviceEndpoints)
    {
        _httpClient = httpClient;

        var address = serviceEndpoints.Value.SpacesService;
        _httpClient.BaseAddress = new Uri(address);
    }

    public async Task<Guid> GetRandomSpaceId() =>
        await _httpClient.GetFromJsonAsync<Guid>("GetRandomSpaceId");

    public async Task<bool> IsSpaceExists(Guid id) =>
        await _httpClient.GetFromJsonAsync<bool>($"IsSpaceExists?spaceId={id}");
    
}
