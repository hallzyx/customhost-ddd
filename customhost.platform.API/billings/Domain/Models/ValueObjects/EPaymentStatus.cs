namespace customhost_backend.billings.Domain.Models.ValueObjects;

public enum EPaymentStatus
{
    Pending,
    Paid,
    Failed,
    Cancelled,
    Refunded
}
