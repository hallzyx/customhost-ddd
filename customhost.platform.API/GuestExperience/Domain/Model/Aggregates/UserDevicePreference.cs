using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Model.Aggregates;

/// <summary>
/// User Device Preference aggregate root that stores personal preferences for IoT devices
/// </summary>
public class UserDevicePreference
{
    public int Id { get; private set; }
    public int? UserId { get; private set; }
    public int DeviceId { get; private set; }
    public string CustomName { get; private set; }
    public string Overrides { get; private set; }
    public DateTime LastUpdated { get; private set; }

    // Navigation properties
    public virtual IoTDevice Device { get; private set; }

    // For EF Core
    protected UserDevicePreference() { }

    public UserDevicePreference(int? userId, int deviceId, string customName, string overrides)
    {
        if (deviceId <= 0)
            throw new ArgumentException("Device ID must be greater than zero", nameof(deviceId));

        UserId = userId;
        DeviceId = deviceId;
        CustomName = customName ?? string.Empty;
        Overrides = overrides ?? "{}";
        LastUpdated = DateTime.UtcNow;
    }

    public UserDevicePreference(CreateUserDevicePreferenceCommand command) : this(command.UserId, command.DeviceId, command.CustomName, command.Overrides)
    {
    }

    public void UpdatePreference(string customName, string overrides)
    {
        CustomName = customName ?? string.Empty;
        Overrides = overrides ?? "{}";
        LastUpdated = DateTime.UtcNow;
    }
}
