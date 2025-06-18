namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Mark Notification As Read Command
/// </summary>
/// <param name="NotificationId">Notification ID</param>
public record MarkNotificationAsReadCommand(int NotificationId);
