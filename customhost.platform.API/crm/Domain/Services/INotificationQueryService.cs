using customhost_backend.crm.Domain.Models.Aggregates;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Notification query service interface
/// </summary>
public interface INotificationQueryService
{
    /// <summary>
    /// Get all notifications
    /// </summary>
    /// <returns>All notifications</returns>
    Task<IEnumerable<Notification>> GetAllAsync();
    
    /// <summary>
    /// Get notification by ID
    /// </summary>
    /// <param name="id">Notification ID</param>
    /// <returns>Notification or null if not found</returns>
    Task<Notification?> GetByIdAsync(int id);
    
    /// <summary>
    /// Get notifications by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User notifications</returns>
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    
    /// <summary>
    /// Get unread notifications by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>Unread user notifications</returns>
    Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId);
}
