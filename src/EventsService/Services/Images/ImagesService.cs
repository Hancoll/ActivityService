using Microsoft.Extensions.Options;
using SC.Internship.Common.ScResult;

namespace EventsService.Services.Images;

public class ImagesService : IImagesService
{
    private readonly HttpClient _httpClient;
    private readonly ImagesServiceEndpoints _endpoints;

    public ImagesService(HttpClient httpClient, IOptions<ImagesServiceEndpoints> options)
    {
        _httpClient = httpClient;
        _endpoints = options.Value;

        _httpClient.BaseAddress = new Uri(_endpoints.BaseAddress);
    }

    public async Task<Guid> GetRandomImageId()
    {
        var result = await _httpClient.GetFromJsonAsync<ScResult<Guid>>(_endpoints.Images);
        return result!.Result;
    }

    public async Task<bool> IsImageExists(Guid id)
    {
        var path = $"{_endpoints.ImageExistence}?imageId={id}";
        var result = await _httpClient.GetFromJsonAsync<ScResult<bool>>(path);
        return result!.Result;
    }
}
