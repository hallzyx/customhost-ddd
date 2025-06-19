using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Booking command service interface
/// </summary>
public interface IBookingCommandService
{
    /// <summary>
    /// Handle create booking command
    /// </summary>
    /// <param name="command">Create booking command</param>
    /// <returns>Created booking or null if failed</returns>
    Task<Booking?> Handle(CreateBookingCommand command);
    
    /// <summary>
    /// Handle update booking command
    /// </summary>
    /// <param name="command">Update booking command</param>
    /// <returns>Updated booking or null if failed</returns>
    Task<Booking?> Handle(UpdateBookingCommand command);
    
    /// <summary>
    /// Handle confirm booking command
    /// </summary>
    /// <param name="command">Confirm booking command</param>
    /// <returns>True if confirmed successfully</returns>
    Task<bool> Handle(ConfirmBookingCommand command);
    
    /// <summary>
    /// Handle check in booking command
    /// </summary>
    /// <param name="command">Check in booking command</param>
    /// <returns>True if checked in successfully</returns>
    Task<bool> Handle(CheckInBookingCommand command);
    
    /// <summary>
    /// Handle check out booking command
    /// </summary>
    /// <param name="command">Check out booking command</param>
    /// <returns>True if checked out successfully</returns>
    Task<bool> Handle(CheckOutBookingCommand command);
    
    /// <summary>
    /// Handle cancel booking command
    /// </summary>
    /// <param name="command">Cancel booking command</param>
    /// <returns>True if cancelled successfully</returns>
    Task<bool> Handle(CancelBookingCommand command);
      /// <summary>
    /// Handle mark booking as no show command
    /// </summary>
    /// <param name="command">Mark booking as no show command</param>
    /// <returns>True if marked as no show successfully</returns>
    Task<bool> Handle(MarkBookingAsNoShowCommand command);
    
    /// <summary>
    /// Handle delete booking command
    /// </summary>
    /// <param name="command">Delete booking command</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> Handle(DeleteBookingCommand command);
}
