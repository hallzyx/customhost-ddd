using customhost_backend.crm.Domain.Models.Aggregates;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Service Request Query Service Interface
/// </summary>
public interface IServiceRequestQueryService
{
    /// <summary>
    /// Get all service requests
    /// </summary>
    /// <returns>All service requests</returns>
    Task<IEnumerable<ServiceRequest>> GetAllAsync();
    
    /// <summary>
    /// Get service request by ID
    /// </summary>
    /// <param name="id">Service request ID</param>
    /// <returns>Service request or null if not found</returns>
    Task<ServiceRequest?> GetByIdAsync(int id);
    
    /// <summary>
    /// Get service requests by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User service requests</returns>
    Task<IEnumerable<ServiceRequest>> GetByUserIdAsync(int userId);
    
    /// <summary>
    /// Get service requests by hotel ID
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <returns>Hotel service requests</returns>
    Task<IEnumerable<ServiceRequest>> GetByHotelIdAsync(int hotelId);
    
    /// <summary>
    /// Get service requests by room ID
    /// </summary>
    /// <param name="roomId">Room ID</param>
    /// <returns>Room service requests</returns>
    Task<IEnumerable<ServiceRequest>> GetByRoomIdAsync(int roomId);
    
    /// <summary>
    /// Get service requests by status
    /// </summary>
    /// <param name="status">Service request status</param>
    /// <returns>Service requests with the specified status</returns>
    Task<IEnumerable<ServiceRequest>> GetByStatusAsync(string status);
}