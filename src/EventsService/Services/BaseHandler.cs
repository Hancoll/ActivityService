using System.Net.Http.Headers;

namespace EventsService.Services;

public class BaseHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization;

        if (authHeader is null)
            return await base.SendAsync(request, cancellationToken);

        request.Headers.Authorization = AuthenticationHeaderValue.Parse(authHeader);

        return await base.SendAsync(request, cancellationToken);
    }
}
