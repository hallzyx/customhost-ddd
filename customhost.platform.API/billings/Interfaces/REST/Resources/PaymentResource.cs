namespace customhost_backend.billings.Interfaces.REST.Resources;

public record PaymentResource(
    int Id,
    int? BookingId,
    int UserId,
    int HotelId,
    int RoomId,
    decimal Amount,
    string Currency,
    DateTime? CheckInDate,
    DateTime? CheckOutDate,
    string PaymentMethod,
    string Status,
    DateTime? PaymentDate,
    DateTime CreatedAt
);
