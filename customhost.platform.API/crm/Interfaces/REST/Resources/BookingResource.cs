namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Booking Resource
/// </summary>
public record BookingResource(
    int Id,
    int UserId,
    int HotelId,
    int RoomId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    string Status,
    decimal TotalPrice,
    string PaymentStatus,
    string? SpecialRequests,
    DateTime CreatedAt,
    int NumberOfNights,
    bool IsActive,
    bool IsCurrentlyCheckedIn,
    string? Preferences = null,
    string? AppliedDevicePreferences = null
);
