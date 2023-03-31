using EventsService.Features.Events;
using EventsService.Features.Tickets.IssueTicketToUser;
using Moq;
using SC.Internship.Common.Exceptions;

namespace EventsService.UnitTests.Tickets;

public class IssueTicket
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;

    [Fact]
    public async Task Handle_Should_ReturnException_WhenTicketsInEventAreNotExist()
    {
        // Arrange

        var @event = new Event();
        var command = new IssueTicketToUserCommand(default, default, null);

        _eventRepositoryMock.Setup(x => x.GetEvent(It.IsAny<Guid>())).Returns(@event);

        var handler = new IssueTicketToUserCommandHandler(_eventRepositoryMock.Object);

        // Act & Assert

        await Assert.ThrowsAsync<ScException>(() => handler.Handle(command, default));
    }

    public IssueTicket()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
    }
}
