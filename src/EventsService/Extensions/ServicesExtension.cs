using EventsService.Services;
using Moq;

namespace EventsService.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var imagesServiceMock = new Mock<IImagesService>();
        var imageId = Guid.NewGuid();
        imagesServiceMock.Setup(x => x.GetRandomImageId()).Returns(imageId);
        imagesServiceMock.Setup(x => x.IsImageExists(imageId)).Returns(true);

        var roomsServiceMock = new Mock<IRoomsService>();
        var roomsId = Guid.NewGuid();
        roomsServiceMock.Setup(x => x.GetRandomRoomId()).Returns(roomsId);
        roomsServiceMock.Setup(x => x.IsRoomExists(roomsId)).Returns(true);

        var usersServiceMock = new Mock<IUsersService>();
        var user = new User("user3243223");
        usersServiceMock.Setup(x => x.GetRandomUser()).Returns(user);
        usersServiceMock.Setup(x => x.IsExists(user.Id)).Returns(true);

        services.AddSingleton<IImagesService>(imagesServiceMock.Object);
        services.AddSingleton<IRoomsService>(roomsServiceMock.Object);
        services.AddSingleton<IUsersService>(usersServiceMock.Object);

        return services;
    }
}
