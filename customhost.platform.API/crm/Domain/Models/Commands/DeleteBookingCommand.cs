namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Delete Booking Command 
/// </summary>
/// <param name="Id">
/// The unique identifier of the booking to delete.
/// </param>
public record DeleteBookingCommand(int Id);
