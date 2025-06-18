using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Aggregates;

/// <summary>
/// Service Request Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Service Request aggregate root.
/// It contains the properties and methods to manage service request information.
/// </remarks>
public class ServiceRequest
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int HotelId { get; private set; }
    public int RoomId { get; private set; }
    public EServiceRequestType Type { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public EServiceRequestStatus Status { get; private set; }
    public EServiceRequestPriority Priority { get; private set; }
    public int? AssignedTo { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string History { get; private set; } = "[]"; // JSON string for history entries

    // For EF Core
    protected ServiceRequest() { }

    public ServiceRequest(CreateServiceRequestCommand command)
    {
        UserId = command.UserId;
        HotelId = command.HotelId;
        RoomId = command.RoomId;
        Type = command.Type;
        Category = command.Category;
        Description = command.Description;
        Status = command.Status;
        Priority = command.Priority;
        AssignedTo = command.AssignedTo;
        CreatedAt = DateTime.UtcNow;
        CompletedAt = null;
        History = "[]";
    }

    public void AssignStaff(int staffId)
    {
        AssignedTo = staffId;
        Status = EServiceRequestStatus.InProgress;
        AddHistoryEntry("Assigned", $"Petition assigned to staff member {staffId}");
    }

    public void Resolve()
    {
        Status = EServiceRequestStatus.Resolved;
        CompletedAt = DateTime.UtcNow;
        AddHistoryEntry("Resolved", "The task has been completed successfully");
    }

    public void Complete()
    {
        Status = EServiceRequestStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        AddHistoryEntry("Completed", "The service request has been completed");
    }

    public void Cancel()
    {
        Status = EServiceRequestStatus.Cancelled;
        AddHistoryEntry("Cancelled", "The service request has been cancelled");
    }

    public void UpdateStatus(EServiceRequestStatus status)
    {
        Status = status;
        if (status == EServiceRequestStatus.Completed || status == EServiceRequestStatus.Resolved)
        {
            CompletedAt = DateTime.UtcNow;
        }
    }    public void UpdatePriority(EServiceRequestPriority priority)
    {
        Priority = priority;
        AddHistoryEntry("Priority Updated", $"Priority changed to {priority}");
    }

    public void UpdateServiceRequest(UpdateServiceRequestCommand command)
    {
        UserId = command.UserId;
        HotelId = command.HotelId;
        RoomId = command.RoomId;
        Type = command.Type;
        Category = command.Category;
        Description = command.Description;
        Status = command.Status;
        Priority = command.Priority;
        AssignedTo = command.AssignedTo;
        
        if (command.Status == EServiceRequestStatus.Completed || command.Status == EServiceRequestStatus.Resolved)
        {
            CompletedAt = DateTime.UtcNow;
        }
        
        AddHistoryEntry("Updated", "Service request information updated");
    }

    private void AddHistoryEntry(string action, string notes)
    {
        var newEntry = new
        {
            timestamp = DateTime.UtcNow.ToString("O"),
            action = action,
            changedBy = AssignedTo ?? UserId,
            notes = notes
        };

        // Simple history management - in a real application, you might want a separate History entity
        var historyEntries = System.Text.Json.JsonSerializer.Deserialize<List<object>>(History) ?? new List<object>();
        historyEntries.Add(newEntry);
        History = System.Text.Json.JsonSerializer.Serialize(historyEntries);
    }

    public bool IsOpen => Status == EServiceRequestStatus.Open;
    public bool IsInProgress => Status == EServiceRequestStatus.InProgress;
    public bool IsCompleted => Status == EServiceRequestStatus.Completed || Status == EServiceRequestStatus.Resolved;
    public bool IsCancelled => Status == EServiceRequestStatus.Cancelled;
}