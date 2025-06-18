namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Create Notification Resource
/// </summary>
public record CreateNotificationResource(
    int UserId,
    string Title,
    string Message,
    string Type = "info"
);
