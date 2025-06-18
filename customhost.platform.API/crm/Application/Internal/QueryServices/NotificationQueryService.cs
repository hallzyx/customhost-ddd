using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;

namespace customhost_backend.crm.Application.Internal.QueryServices;

/// <summary>
/// Notification Query Service
/// </summary>
/// <remarks>
/// This class represents the notification query service.
/// It contains the methods to handle notification queries.
/// </remarks>
public class NotificationQueryService(INotificationRepository notificationRepository) 
    : INotificationQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Notification>> GetAllAsync()
    {
        return await notificationRepository.GetAllAsync();
    }

    /// <inheritdoc />
    public async Task<Notification?> GetByIdAsync(int id)
    {
        return await notificationRepository.GetByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId)
    {
        return await notificationRepository.GetByUserIdAsync(userId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId)
    {
        var notifications = await notificationRepository.GetByUserIdAsync(userId);
        return notifications.Where(n => !n.Read);
    }
}
