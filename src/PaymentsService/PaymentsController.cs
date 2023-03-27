using Microsoft.AspNetCore.Mvc;

namespace PaymentsService;

[Route("/")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly List<PaymentOperation> _paymentOperations = new();

    [HttpPost("create")]
    public IActionResult CreatePaymentOperation()
    {
        var id = Guid.NewGuid();

        var paymentOperation = new PaymentOperation 
        { 
            Id = id,
            State = PaymentState.Hold,
            DateCreation = DateTime.UtcNow
        };

        _paymentOperations.Add(paymentOperation);

        return Ok(id);
    }

    [HttpPost("confirm/{paymentId:guid}")]
    public IActionResult ConfirmPaymentOperation(Guid paymentId)
    {
        var paymentOperation = _paymentOperations.First(x => x.Id == paymentId);

        paymentOperation.State = PaymentState.Confirmed;
        paymentOperation.DateConfirmation = DateTime.UtcNow;

        return Ok();
    }

    [HttpPost("cancel/{paymentId:guid}")]
    public IActionResult CancelPaymentOperation(Guid paymentId)
    {
        var paymentOperation = _paymentOperations.First(x => x.Id == paymentId);

        paymentOperation.State = PaymentState.Canceled;
        paymentOperation.DateCancellation = DateTime.UtcNow;

        return Ok();
    }
}
