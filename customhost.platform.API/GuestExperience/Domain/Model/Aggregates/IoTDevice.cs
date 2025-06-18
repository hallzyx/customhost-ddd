using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Model.Aggregates;

/// <summary>
/// IoT Device aggregate root that represents a smart device available in the hotel
/// </summary>
public class IoTDevice
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string DeviceType { get; private set; }
    public string ConfigSchema { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // For EF Core
    protected IoTDevice() { }

    public IoTDevice(string name, string deviceType, string configSchema)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Device name cannot be null or empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(deviceType))
            throw new ArgumentException("Device type cannot be null or empty", nameof(deviceType));

        Name = name;
        DeviceType = deviceType;
        ConfigSchema = configSchema ?? "{}";
        Status = "active";
        CreatedAt = DateTime.UtcNow;
    }

    public IoTDevice(CreateIoTDeviceCommand command) : this(command.Name, command.DeviceType, command.ConfigSchema)
    {
    }

    public void UpdateDevice(string name, string deviceType, string configSchema)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Device name cannot be null or empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(deviceType))
            throw new ArgumentException("Device type cannot be null or empty", nameof(deviceType));

        Name = name;
        DeviceType = deviceType;
        ConfigSchema = configSchema ?? "{}";
    }

    public void UpdateStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be null or empty", nameof(status));

        Status = status;
    }
}
