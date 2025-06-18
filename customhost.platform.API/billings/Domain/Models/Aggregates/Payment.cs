using customhost_backend.billings.Domain.Models.Commands;
using customhost_backend.billings.Domain.Models.ValueObjects;

namespace customhost_backend.billings.Domain.Models.Aggregates;

/// <summary>
/// Payment Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Payment aggregate root.
/// It contains the properties and methods to manage payment information.
/// </remarks>
public class Payment
{
    public int Id { get; private set; }
    public int? BookingId { get; private set; }
    public int UserId { get; private set; }
    public int HotelId { get; private set; }
    public int RoomId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public DateTime? CheckInDate { get; private set; }
    public DateTime? CheckOutDate { get; private set; }
    public EPaymentMethod PaymentMethod { get; private set; }
    public EPaymentStatus Status { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // For EF Core
    protected Payment() { }

    public Payment(CreatePaymentCommand command)
    {
        BookingId = command.BookingId;
        UserId = command.UserId;
        HotelId = command.HotelId;
        RoomId = command.RoomId;
        Amount = command.Amount;
        Currency = command.Currency;
        CheckInDate = command.CheckInDate;
        CheckOutDate = command.CheckOutDate;
        PaymentMethod = command.PaymentMethod;
        Status = command.Status;
        PaymentDate = Status == EPaymentStatus.Paid ? DateTime.UtcNow : null;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdatePayment(UpdatePaymentCommand command)
    {
        if (command.Status.HasValue)
        {
            Status = command.Status.Value;
            if (Status == EPaymentStatus.Paid && PaymentDate == null)
            {
                PaymentDate = DateTime.UtcNow;
            }
        }

        if (command.PaymentMethod.HasValue)
        {
            PaymentMethod = command.PaymentMethod.Value;
        }

        if (command.PaymentDate.HasValue)
        {
            PaymentDate = command.PaymentDate.Value;
        }
    }

    public void MarkAsPaid()
    {
        Status = EPaymentStatus.Paid;
        PaymentDate = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = EPaymentStatus.Failed;
    }

    public void Cancel()
    {
        if (Status == EPaymentStatus.Pending)
        {
            Status = EPaymentStatus.Cancelled;
        }
    }

    public void Refund()
    {
        if (Status == EPaymentStatus.Paid)
        {
            Status = EPaymentStatus.Refunded;
        }
    }

    // Business rules
    public bool IsPaid => Status == EPaymentStatus.Paid;
    public bool IsPending => Status == EPaymentStatus.Pending;
    public bool IsFailed => Status == EPaymentStatus.Failed;
    public bool IsCancelled => Status == EPaymentStatus.Cancelled;
    public bool IsRefunded => Status == EPaymentStatus.Refunded;    public string FormattedAmount => Amount.ToString("C", new System.Globalization.CultureInfo("en-US"));
}
