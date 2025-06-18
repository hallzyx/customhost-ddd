namespace customhost_backend.crm.Domain.Models.Queries;

/// <summary>
/// Notification Queries
/// </summary>
public static class NotificationQueries
{
    /// <summary>
    /// Get all notifications query
    /// </summary>
    /// <returns>All notifications query</returns>
    public static string GetAllNotifications()
    {
        return "SELECT * FROM notifications ORDER BY created_at DESC";
    }

    /// <summary>
    /// Get notification by ID query
    /// </summary>
    /// <param name="id">Notification ID</param>
    /// <returns>Notification by ID query</returns>
    public static string GetNotificationById(int id)
    {
        return $"SELECT * FROM notifications WHERE id = {id}";
    }

    /// <summary>
    /// Get notifications by user ID query
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>Notifications by user ID query</returns>
    public static string GetNotificationsByUserId(int userId)
    {
        return $"SELECT * FROM notifications WHERE user_id = {userId} ORDER BY created_at DESC";
    }

    /// <summary>
    /// Get unread notifications by user ID query
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>Unread notifications by user ID query</returns>
    public static string GetUnreadNotificationsByUserId(int userId)
    {
        return $"SELECT * FROM notifications WHERE user_id = {userId} AND read = 0 ORDER BY created_at DESC";
    }
}
