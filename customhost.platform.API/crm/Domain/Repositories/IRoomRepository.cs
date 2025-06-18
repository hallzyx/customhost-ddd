using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Domain.Repositories;

/// <summary>
/// Room Repository Interface
/// </summary>
public interface IRoomRepository : IBaseRepository<Room>
{
    /// <summary>
    /// Find room by room number
    /// </summary>
    /// <param name="roomNumber">Room number</param>
    /// <returns>Room if found, null otherwise</returns>
    Task<Room?> FindByRoomNumberAsync(int roomNumber);
    
    /// <summary>
    /// Find rooms by hotel ID
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <returns>List of rooms in the hotel</returns>
    Task<IEnumerable<Room>> FindByHotelIdAsync(int hotelId);
    
    /// <summary>
    /// Check if room number exists in hotel
    /// </summary>
    /// <param name="roomNumber">Room number</param>
    /// <param name="hotelId">Hotel ID</param>
    /// <returns>True if exists, false otherwise</returns>
    Task<bool> ExistsByRoomNumberAndHotelIdAsync(int roomNumber, int hotelId);
    
    /// <summary>
    /// Check if room number exists in hotel (excluding specific room ID)
    /// </summary>
    /// <param name="roomNumber">Room number</param>
    /// <param name="hotelId">Hotel ID</param>
    /// <param name="excludeRoomId">Room ID to exclude from check</param>
    /// <returns>True if exists, false otherwise</returns>
    Task<bool> ExistsByRoomNumberAndHotelIdAsync(int roomNumber, int hotelId, int excludeRoomId);
}