using EventsService.Services;
using EventsService.Services.Images;
using EventsService.Services.Payments;
using EventsService.Services.Spaces;
using EventsService.Services.Users;
using Moq;
using Polly;
using Polly.Extensions.Http;

namespace EventsService.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<ImagesServiceEndpoints>(configuration.GetSection(ImagesServiceEndpoints.SectionName));
        services.Configure<SpacesServiceEndpoints>(configuration.GetSection(SpacesServiceEndpoints.SectionName));
        services.Configure<PaymentsServiceEndpoints>(configuration.GetSection(PaymentsServiceEndpoints.SectionName));

        var usersServiceMock = new Mock<IUsersService>();
        var user = new User("user3243223");
        usersServiceMock.Setup(x => x.GetRandomUser()).Returns(user);
        usersServiceMock.Setup(x => x.IsExists(user.Id)).Returns(true);

        services.AddSingleton(usersServiceMock.Object);

        services.AddTransient<BaseHandler>();

        services.AddHttpClient<ISpacesService, SpacesService>()
            .AddHttpMessageHandler<BaseHandler>()
            .AddPolicyHandler(GetRetryPolicy());

        services.AddHttpClient<IImagesService, ImagesService>()
            .AddHttpMessageHandler<BaseHandler>()
            .AddPolicyHandler(GetRetryPolicy());

        services.AddHttpClient<IPaymentsService, PaymentService>()
            .AddHttpMessageHandler<BaseHandler>()
            .AddPolicyHandler(GetRetryPolicy());

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
