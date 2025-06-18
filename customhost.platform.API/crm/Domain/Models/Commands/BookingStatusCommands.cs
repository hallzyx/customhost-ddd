namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Cancel Booking Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the booking to cancel.
/// </param>
public record CancelBookingCommand(int Id);

/// <summary>
/// Check In Booking Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the booking to check in.
/// </param>
public record CheckInBookingCommand(int Id);

/// <summary>
/// Check Out Booking Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the booking to check out.
/// </param>
public record CheckOutBookingCommand(int Id);

/// <summary>
/// Confirm Booking Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the booking to confirm.
/// </param>
public record ConfirmBookingCommand(int Id);

/// <summary>
/// Mark Booking as No Show Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the booking to mark as no show.
/// </param>
public record MarkBookingAsNoShowCommand(int Id);
