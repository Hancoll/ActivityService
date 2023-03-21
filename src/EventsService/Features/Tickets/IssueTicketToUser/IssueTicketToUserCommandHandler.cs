using EventsService.Features.Events;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;

namespace EventsService.Features.Tickets.IssueTicketToUser;

[UsedImplicitly]
public class IssueTicketToUserCommandHandler : IRequestHandler<IssueTicketToUserCommand, Ticket>
{
    private readonly IEventRepository _eventRepository;

    public async Task<Ticket> Handle(IssueTicketToUserCommand request, CancellationToken cancellationToken)
    {
        var @event = _eventRepository.GetEvent(request.EventId);
        Ticket? ticket = null;

        if (@event.HasPlaces)
        {
            if(request.Place is null) throw new ArgumentNullException(nameof(request.Place));

            ticket = @event.Tickets[(int)request.Place - 1];
            ticket.Place = request.Place;
            ticket.Owner = request.UserId;
            _eventRepository.Update(@event);
            return ticket;
        }

        foreach (var t in @event.Tickets)
        {
            if (t.Owner is not null)
                continue;

            ticket = t;
            ticket.Owner = request.UserId;
            _eventRepository.Update(@event);
            return ticket;
        }

        throw new ScException(message: "No free tickets.");
    }

    public IssueTicketToUserCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
