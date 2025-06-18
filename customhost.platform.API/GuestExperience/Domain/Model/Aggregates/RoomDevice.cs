using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Model.Aggregates;

/// <summary>
/// Room Device aggregate root that represents an IoT device assigned to a specific room
/// </summary>
public class RoomDevice
{
    public int Id { get; private set; }
    public int RoomId { get; private set; }
    public int IoTDeviceId { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    public virtual IoTDevice IoTDevice { get; private set; }

    // For EF Core
    protected RoomDevice() { }

    public RoomDevice(int roomId, int iotDeviceId, string status = "working")
    {
        if (roomId <= 0)
            throw new ArgumentException("Room ID must be greater than zero", nameof(roomId));
        
        if (iotDeviceId <= 0)
            throw new ArgumentException("IoT Device ID must be greater than zero", nameof(iotDeviceId));

        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be null or empty", nameof(status));

        RoomId = roomId;
        IoTDeviceId = iotDeviceId;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public RoomDevice(CreateRoomDeviceCommand command) : this(command.RoomId, command.IoTDeviceId, command.Status)
    {
    }

    public void UpdateStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be null or empty", nameof(status));

        Status = status;
    }
}
