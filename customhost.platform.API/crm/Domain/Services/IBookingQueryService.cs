using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Booking query service interface
/// </summary>
public interface IBookingQueryService
{
    /// <summary>
    /// Handle get all bookings query
    /// </summary>
    /// <param name="query">Get all bookings query</param>
    /// <returns>List of all bookings</returns>
    Task<IEnumerable<Booking>> Handle(GetAllBookingsQuery query);
    
    /// <summary>
    /// Handle get booking by id query
    /// </summary>
    /// <param name="query">Get booking by id query</param>
    /// <returns>Booking if found</returns>
    Task<Booking?> Handle(GetBookingByIdQuery query);
    
    /// <summary>
    /// Handle get bookings by user id query
    /// </summary>
    /// <param name="query">Get bookings by user id query</param>
    /// <returns>List of bookings for the user</returns>
    Task<IEnumerable<Booking>> Handle(GetBookingsByUserIdQuery query);
    
    /// <summary>
    /// Handle get bookings by hotel id query
    /// </summary>
    /// <param name="query">Get bookings by hotel id query</param>
    /// <returns>List of bookings for the hotel</returns>
    Task<IEnumerable<Booking>> Handle(GetBookingsByHotelIdQuery query);
    
    /// <summary>
    /// Handle get bookings by room id query
    /// </summary>
    /// <param name="query">Get bookings by room id query</param>
    /// <returns>List of bookings for the room</returns>
    Task<IEnumerable<Booking>> Handle(GetBookingsByRoomIdQuery query);
    
    /// <summary>
    /// Handle get bookings by status query
    /// </summary>
    /// <param name="query">Get bookings by status query</param>
    /// <returns>List of bookings with the specified status</returns>
    Task<IEnumerable<Booking>> Handle(GetBookingsByStatusQuery query);
    
    /// <summary>
    /// Handle get bookings by date range query
    /// </summary>
    /// <param name="query">Get bookings by date range query</param>
    /// <returns>List of bookings within the date range</returns>
    Task<IEnumerable<Booking>> Handle(GetBookingsByDateRangeQuery query);
}
