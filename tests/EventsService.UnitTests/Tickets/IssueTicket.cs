using EventsService.Features.Events;
using EventsService.Features.Tickets.IssueTicketToUser;
using Moq;

namespace EventsService.UnitTests.Tickets;

public class IssueTicket
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;

    [Fact]
    public async Task Handle_Should_ReturnException_WhenTicketsInEventAreNotExist()
    {
        // Arrange

        var @event = new Event(
            default,
            default,
            default!,
            default!,
            default!,
            default,
            default,
            default!,
            default,
            default);

        var command = new IssueTicketToUserCommand(default, default, null);

        _eventRepositoryMock.Setup(x => x.GetEvent(It.IsAny<Guid>())).Returns(@event);

        var handler = new IssueTicketToUserCommandHandler(_eventRepositoryMock.Object);

        // Act

        try
        {
            await handler.Handle(command, default);
        }

        // Assert

        catch
        {
            Assert.True(true);
            return;
        }

        Assert.True(false);
    }

    public IssueTicket()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
    }
}
