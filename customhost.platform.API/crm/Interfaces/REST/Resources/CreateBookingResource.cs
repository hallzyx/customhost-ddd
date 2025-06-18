namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Create Booking Resource
/// </summary>
public record CreateBookingResource(
    int UserId,
    int HotelId,
    int RoomId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    decimal TotalPrice,
    string? SpecialRequests = null,
    string? Preferences = null,
    string? AppliedDevicePreferences = null
);
