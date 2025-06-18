using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Domain.Repositories;

/// <summary>
/// Booking repository interface
/// </summary>
public interface IBookingRepository : IBaseRepository<Booking>
{
    /// <summary>
    /// Find bookings by user ID
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <returns>List of bookings for the user</returns>
    Task<IEnumerable<Booking>> FindByUserIdAsync(int userId);
    
    /// <summary>
    /// Find bookings by hotel ID
    /// </summary>
    /// <param name="hotelId">The hotel ID</param>
    /// <returns>List of bookings for the hotel</returns>
    Task<IEnumerable<Booking>> FindByHotelIdAsync(int hotelId);
    
    /// <summary>
    /// Find bookings by room ID
    /// </summary>
    /// <param name="roomId">The room ID</param>
    /// <returns>List of bookings for the room</returns>
    Task<IEnumerable<Booking>> FindByRoomIdAsync(int roomId);
    
    /// <summary>
    /// Find bookings by status
    /// </summary>
    /// <param name="status">The booking status</param>
    /// <returns>List of bookings with the specified status</returns>
    Task<IEnumerable<Booking>> FindByStatusAsync(BookingStatus status);
    
    /// <summary>
    /// Find bookings within a date range
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of bookings within the date range</returns>
    Task<IEnumerable<Booking>> FindByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Check if room is available for the given date range
    /// </summary>
    /// <param name="roomId">The room ID</param>
    /// <param name="checkInDate">Check-in date</param>
    /// <param name="checkOutDate">Check-out date</param>
    /// <param name="excludeBookingId">Optional booking ID to exclude from check</param>
    /// <returns>True if room is available</returns>
    Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate, int? excludeBookingId = null);
}
