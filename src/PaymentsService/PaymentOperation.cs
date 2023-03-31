namespace PaymentsService;

public class PaymentOperation
{
    public Guid Id { get; set; }

    public PaymentState State { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime DateConfirmation { get; set; }

    public DateTime DateCancellation { get; set; }

    public string Description { get; set; } = null!;
}

public enum PaymentState { Hold, Confirmed, Canceled }
