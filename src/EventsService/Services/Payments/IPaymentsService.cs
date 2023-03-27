namespace EventsService.Services.Payments;

public interface IPaymentsService
{
    Task<Guid> CreatePaymentOperation();

    Task ConfirmPaymentOperation(Guid paymentId);

    Task CancelPaymentOperation(Guid paymentId);
}
