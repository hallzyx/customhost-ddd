using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;

namespace customhost_backend.profiles.Domain.Services;

/// <summary>
/// User Query Service Interface
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>All users</returns>
    Task<IEnumerable<User>> GetAllAsync();
    
    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User or null if not found</returns>
    Task<User?> GetByIdAsync(int id);
    
    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email">User email</param>
    /// <returns>User or null if not found</returns>
    Task<User?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Get users by hotel ID
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <returns>Hotel users</returns>
    Task<IEnumerable<User>> GetByHotelIdAsync(int hotelId);
    
    /// <summary>
    /// Get users by role
    /// </summary>
    /// <param name="role">User role</param>
    /// <returns>Users with the specified role</returns>
    Task<IEnumerable<User>> GetByRoleAsync(EUserRole role);
}
