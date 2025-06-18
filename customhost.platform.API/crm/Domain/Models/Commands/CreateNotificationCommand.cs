namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Create Notification Command
/// </summary>
/// <param name="UserId">User ID</param>
/// <param name="Title">Notification title</param>
/// <param name="Message">Notification message</param>
/// <param name="Type">Notification type (info, alert, success, warning)</param>
public record CreateNotificationCommand(
    int UserId,
    string Title,
    string Message,
    string Type = "info"
);
