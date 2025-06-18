using customhost_backend.billings.Domain.Models.ValueObjects;

namespace customhost_backend.billings.Domain.Models.Commands;

public record CreatePaymentCommand(
    int? BookingId,
    int UserId,
    int HotelId,
    int RoomId,
    decimal Amount,
    string Currency,
    DateTime? CheckInDate,
    DateTime? CheckOutDate,
    EPaymentMethod PaymentMethod,
    EPaymentStatus Status = EPaymentStatus.Pending
);
