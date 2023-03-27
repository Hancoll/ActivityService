using EventsService.Features.Events;
using EventsService.Features.Tickets.IssueTicketToUser;
using EventsService.Services.Payments;
using JetBrains.Annotations;
using MediatR;
using System.Diagnostics;

namespace EventsService.Features.Tickets.SellTicketToUser;

[UsedImplicitly]
public class SellTicketToUserCommandHandler : IRequestHandler<SellTicketToUserCommand, Ticket>
{
    private readonly IPaymentsService _paymentsService;
    private readonly IEventRepository _eventRepository;

    public async Task<Ticket> Handle(SellTicketToUserCommand request, CancellationToken cancellationToken)
    {
        var paymentOperationId = await _paymentsService.CreatePaymentOperation();

        Debug.WriteLine(paymentOperationId);

        var command = new IssueTicketToUserCommand(request.UserId, request.EventId, request.Place);
        var handler = new IssueTicketToUserCommandHandler(_eventRepository);
        Ticket ticket;

        try
        {
            ticket = await handler.Handle(command, cancellationToken);
        }

        catch
        {
            await _paymentsService.CancelPaymentOperation(paymentOperationId);
            throw;
        }

        await _paymentsService.ConfirmPaymentOperation(paymentOperationId);
        return ticket;
    }

    public SellTicketToUserCommandHandler(IPaymentsService paymentsService, IEventRepository eventRepository)
    {
        _paymentsService = paymentsService;
        _eventRepository = eventRepository;
    }
}
