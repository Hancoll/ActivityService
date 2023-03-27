using EventsService.Features.Events.AddEvent;
using EventsService.Services.Images;
using EventsService.Services.Spaces;
using Moq;

namespace EventsService.UnitTests.Events;

public class AddEvent
{
    private readonly Mock<IImagesService> _imagesServiceMock;
    private readonly Mock<ISpacesService> _spacesServiceMock;

    [Fact]
    public void Validator_Should_ReturnFailureResult_WhenImageOrRoomIsNotExists()
    {
        // Arrange

        var imageGuid = Guid.NewGuid();
        var roomGuid = Guid.NewGuid();

        var command = new AddEventCommand(
            DateTime.Now,
            DateTime.Now.AddHours(1),
            "Some name",
            "Some desc",
            imageGuid,
            roomGuid,
            false,
            null);

        _imagesServiceMock.Setup(x => x.IsImageExists(It.IsAny<Guid>())).Returns(Task.FromResult(false));
        _spacesServiceMock.Setup(x => x.IsSpaceExists(It.IsAny<Guid>())).Returns(Task.FromResult(false));

        var validator = new AddEventCommandValidator(_imagesServiceMock.Object, _spacesServiceMock.Object);

        // Act

        var result = validator.Validate(command);

        // Assert

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validator_Should_ReturnFailureResult_WhenEndDateGreaterOrEqualThanStartDate()
    {
        // Arrange

        var startDateTime = DateTime.Now.AddMinutes(1);
        var endDateTime = DateTime.Now;

        var command = new AddEventCommand(
            startDateTime,
            endDateTime,
            "Some name",
            "Some desc",
            Guid.NewGuid(),
            Guid.NewGuid(),
            false,
            null);

        _imagesServiceMock.Setup(x => x.IsImageExists(It.IsAny<Guid>())).Returns(Task.FromResult(false));
        _spacesServiceMock.Setup(x => x.IsSpaceExists(It.IsAny<Guid>())).Returns(Task.FromResult(true));

        var validator = new AddEventCommandValidator(_imagesServiceMock.Object, _spacesServiceMock.Object);

        // Act

        var result = validator.Validate(command);

        // Asserts

        Assert.False(result.IsValid);
    }

    public AddEvent()
    {
        _imagesServiceMock = new Mock<IImagesService>();
        _spacesServiceMock = new Mock<ISpacesService>();
    }
}
