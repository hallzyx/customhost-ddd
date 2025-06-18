namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Update Booking Resource
/// </summary>
public record UpdateBookingResource(
    DateTime CheckInDate,
    DateTime CheckOutDate,
    decimal TotalPrice,
    string? SpecialRequests = null
);
