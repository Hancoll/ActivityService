using Microsoft.Extensions.Options;
using SC.Internship.Common.ScResult;

namespace EventsService.Services.Spaces;

public class SpacesService : ISpacesService
{
    private readonly HttpClient _httpClient;
    private readonly SpacesServiceEndpoints _endpoints;

    public SpacesService(HttpClient httpClient, IOptions<SpacesServiceEndpoints> options)
    {
        _httpClient = httpClient;
        _endpoints = options.Value;

        _httpClient.BaseAddress = new Uri(_endpoints.BaseAddress);
    }

    public async Task<Guid> GetRandomSpaceId()
    {
        var result = await _httpClient.GetFromJsonAsync<ScResult<Guid>>(_endpoints.Spaces);
        return result!.Result;
    }

    public async Task<bool> IsSpaceExists(Guid id)
    {
        var path = $"{_endpoints.SpaceExistence}?spaceId={id}";
        var result = await _httpClient.GetFromJsonAsync<ScResult<bool>>(path);
        return result!.Result;
    }
}
