using System.ComponentModel.DataAnnotations;

namespace customhost_backend.analytics.Domain.Models.Aggregates;

/// <summary>
/// Analytics Snapshot aggregate representing a point-in-time capture of system state
/// </summary>
public class AnalyticsSnapshot
{
    [Key] public int Id { get; private set; }
    
    [Required] public DateTime Timestamp { get; private set; }
    
    [Required] [MaxLength(100)] public string SnapshotType { get; private set; }
    
    [Required] public string Data { get; private set; } // JSON data
    
    [Required] public DateTime ExpiresAt { get; private set; }
    
    public DateTime CreatedAt { get; private set; }

    // Parameterless constructor for EF Core
    protected AnalyticsSnapshot() { }

    public AnalyticsSnapshot(string snapshotType, string data, TimeSpan expirationTime)
    {
        SnapshotType = snapshotType ?? throw new ArgumentNullException(nameof(snapshotType));
        Data = data ?? throw new ArgumentNullException(nameof(data));
        Timestamp = DateTime.UtcNow;
        ExpiresAt = DateTime.UtcNow.Add(expirationTime);
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateData(string newData)
    {
        Data = newData ?? throw new ArgumentNullException(nameof(newData));
        Timestamp = DateTime.UtcNow;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpiresAt;
}
