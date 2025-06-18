namespace customhost_backend.crm.Domain.Models.ValueObjects;

public enum BookingStatus
{
    Pending,
    Confirmed,
    CheckedIn,
    CheckedOut,
    Cancelled,
    NoShow
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Partial,
    Failed,
    Refunded
}
