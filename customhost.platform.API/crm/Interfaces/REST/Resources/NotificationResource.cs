namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Notification Resource
/// </summary>
public record NotificationResource(
    int Id,
    int UserId,
    string Title,
    string Message,
    string Type,
    bool Read,
    DateTime CreatedAt
);
