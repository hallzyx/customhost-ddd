namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Create Booking Command 
/// </summary>
/// <param name="UserId">
/// The user ID making the booking.
/// </param>
/// <param name="HotelId">
/// The hotel ID for the booking.
/// </param>
/// <param name="RoomId">
/// The room ID for the booking.
/// </param>
/// <param name="CheckInDate">
/// The check-in date for the booking.
/// </param>
/// <param name="CheckOutDate">
/// The check-out date for the booking.
/// </param>
/// <param name="TotalPrice">
/// The total price for the booking.
/// </param>
/// <param name="SpecialRequests">
/// Any special requests for the booking.
/// </param>
/// <param name="Preferences">
/// JSON string containing user preferences.
/// </param>
/// <param name="AppliedDevicePreferences">
/// JSON string containing applied device preferences.
/// </param>
public record CreateBookingCommand(
    int UserId,
    int HotelId,
    int RoomId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    decimal TotalPrice,
    string? SpecialRequests = null,
    string? Preferences = null,
    string? AppliedDevicePreferences = null)
{
    public void Validate()
    {
        if (UserId <= 0)
            throw new ArgumentException("Valid user ID is required", nameof(UserId));
            
        if (HotelId <= 0)
            throw new ArgumentException("Valid hotel ID is required", nameof(HotelId));
            
        if (RoomId <= 0)
            throw new ArgumentException("Valid room ID is required", nameof(RoomId));
            
        if (CheckInDate >= CheckOutDate)
            throw new ArgumentException("Check-in date must be before check-out date", nameof(CheckInDate));
            
        if (CheckInDate.Date < DateTime.Now.Date)
            throw new ArgumentException("Check-in date cannot be in the past", nameof(CheckInDate));
            
        if (TotalPrice <= 0)
            throw new ArgumentException("Total price must be greater than zero", nameof(TotalPrice));
            
        if (SpecialRequests?.Length > 1000)
            throw new ArgumentException("Special requests cannot exceed 1000 characters", nameof(SpecialRequests));
    }
};
