namespace customhost_backend.crm.Domain.Models.Aggregates;

/// <summary>
/// Notification Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Notification aggregate root.
/// It contains the properties and methods to manage notification information.
/// </remarks>
public class Notification
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty; // info, alert, success, warning
    public bool Read { get; private set; } = false;
    public DateTime CreatedAt { get; private set; }

    // For EF Core
    protected Notification()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public Notification(int userId, string title, string message, string type = "info")
    {
        UserId = userId;
        Title = title;
        Message = message;
        Type = type;
        Read = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsRead()
    {
        Read = true;
    }
}
