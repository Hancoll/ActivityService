using EventsService.Services;
using EventsService.Services.Images;
using EventsService.Services.Payments;
using EventsService.Services.Spaces;
using EventsService.Services.Users;
using Moq;

namespace EventsService.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<ServiceEndpoints>(configuration.GetSection(ServiceEndpoints.SectionName));

        var usersServiceMock = new Mock<IUsersService>();
        var user = new User("user3243223");
        usersServiceMock.Setup(x => x.GetRandomUser()).Returns(user);
        usersServiceMock.Setup(x => x.IsExists(user.Id)).Returns(true);

        services.AddSingleton(usersServiceMock.Object);

        services.AddTransient<BaseHandler>();

        services.AddHttpClient<ISpacesService, SpacesService>().AddHttpMessageHandler<BaseHandler>();
        services.AddHttpClient<IImagesService, ImagesService>().AddHttpMessageHandler<BaseHandler>();
        services.AddHttpClient<IPaymentsService, PaymentService>().AddHttpMessageHandler<BaseHandler>();

        return services;
    }
}
