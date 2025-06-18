using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Notification command service interface
/// </summary>
public interface INotificationCommandService
{
    /// <summary>
    /// Handle create notification command
    /// </summary>
    /// <param name="command">Create notification command</param>
    /// <returns>Created notification or null if failed</returns>
    Task<Notification?> Handle(CreateNotificationCommand command);
    
    /// <summary>
    /// Handle mark notification as read command
    /// </summary>
    /// <param name="command">Mark notification as read command</param>
    /// <returns>Updated notification or null if failed</returns>
    Task<Notification?> Handle(MarkNotificationAsReadCommand command);
}
