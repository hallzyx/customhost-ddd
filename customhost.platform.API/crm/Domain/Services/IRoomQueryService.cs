using customhost_backend.crm.Domain.Models.Aggregates;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Room Query Service Interface
/// </summary>
public interface IRoomQueryService
{
    /// <summary>
    /// Get all rooms
    /// </summary>
    /// <returns>All rooms</returns>
    Task<IEnumerable<Room>> GetAllAsync();
    
    /// <summary>
    /// Get room by ID
    /// </summary>
    /// <param name="id">Room ID</param>
    /// <returns>Room or null if not found</returns>
    Task<Room?> GetByIdAsync(int id);
    
    /// <summary>
    /// Get rooms by hotel ID
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <returns>Rooms in the hotel</returns>
    Task<IEnumerable<Room>> GetByHotelIdAsync(int hotelId);
    
    /// <summary>
    /// Get room by room number
    /// </summary>
    /// <param name="roomNumber">Room number</param>
    /// <returns>Room or null if not found</returns>
    Task<Room?> GetByRoomNumberAsync(int roomNumber);
}