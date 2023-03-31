using EventsService.Features.Events;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;

namespace EventsService.Features.Tickets.IssueTicketToUser;

[UsedImplicitly]
public class IssueTicketToUserCommandHandler : IRequestHandler<IssueTicketToUserCommand, Ticket>
{
    private readonly IEventRepository _eventRepository;

    public Task<Ticket> Handle(IssueTicketToUserCommand request, CancellationToken cancellationToken)
    {
        var @event = _eventRepository.GetEvent(request.EventId);
        Ticket? ticket;

        if (@event.Tickets is null)
            throw new ScException(message: "The event has no tickets.");

        if (@event.HasPlaces)
        {
            if (request.Place is null) 
                throw new ArgumentNullException(nameof(request.Place));

            var index = (int)request.Place - 1;

            if (index < 0 || index >= @event.Tickets.Count)
                throw new ScException("The ticket does not exist.");

            ticket = @event.Tickets[index];

            if (ticket.Owner is not null)
                throw new ScException(message: "The ticket already has an owner.");

            ticket.Place = request.Place;
            ticket.Owner = request.UserId;
            _eventRepository.Update(@event);

            return Task.FromResult(ticket);
        }

        foreach (var t in @event.Tickets.Where(t => t.Owner is null))
        {
            ticket = t;
            ticket.Owner = request.UserId;
            _eventRepository.Update(@event);

            return Task.FromResult(ticket);
        }

        throw new ScException(message: "No free tickets.");
    }

    public IssueTicketToUserCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
