using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace PaymentsService;

[Route("/")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private static readonly List<PaymentOperation> PaymentOperations = new();
    
    [HttpPost]
    public ScResult<Guid> CreatePaymentOperation()
    {
        var id = Guid.NewGuid();

        var paymentOperation = new PaymentOperation 
        { 
            Id = id,
            State = PaymentState.Hold,
            DateCreation = DateTime.UtcNow
        };

        PaymentOperations.Add(paymentOperation);

        return new ScResult<Guid>(id);
    }

    [HttpPost("confirmation/{paymentId:guid}")]
    public ScResult ConfirmPaymentOperation(Guid paymentId)
    {
        var paymentOperation = PaymentOperations.First(x => x.Id == paymentId);

        paymentOperation.State = PaymentState.Confirmed;
        paymentOperation.DateConfirmation = DateTime.UtcNow;

        return new ScResult();
    }

    [HttpPost("cancellation/{paymentId:guid}")]
    public ScResult CancelPaymentOperation(Guid paymentId)
    {
        var paymentOperation = PaymentOperations.First(x => x.Id == paymentId);

        paymentOperation.State = PaymentState.Canceled;
        paymentOperation.DateCancellation = DateTime.UtcNow;

        return new ScResult();
    }
}
