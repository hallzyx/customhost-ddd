namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Update Booking Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the booking.
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
public record UpdateBookingCommand(
    int Id,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    decimal TotalPrice,
    string? SpecialRequests = null)
{
    public void Validate()
    {
        if (Id <= 0)
            throw new ArgumentException("Valid booking ID is required", nameof(Id));
            
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
