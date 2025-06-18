using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Model.Aggregates;

/// <summary>
/// Room Device Preference aggregate root that stores configuration preferences for a specific room device
/// </summary>
public class RoomDevicePreference
{
    public int Id { get; private set; }
    public int RoomDeviceId { get; private set; }
    public string Preferences { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdated { get; private set; }

    // Navigation properties
    public virtual RoomDevice RoomDevice { get; private set; }

    // For EF Core
    protected RoomDevicePreference() { }

    public RoomDevicePreference(int roomDeviceId, string preferences)
    {
        if (roomDeviceId <= 0)
            throw new ArgumentException("Room Device ID must be greater than zero", nameof(roomDeviceId));

        RoomDeviceId = roomDeviceId;
        Preferences = preferences ?? "{}";
        CreatedAt = DateTime.UtcNow;
        LastUpdated = DateTime.UtcNow;
    }

    public RoomDevicePreference(CreateRoomDevicePreferenceCommand command) : this(command.RoomDeviceId, command.Preferences)
    {
    }

    public void UpdatePreferences(string preferences)
    {
        Preferences = preferences ?? "{}";
        LastUpdated = DateTime.UtcNow;
    }
}
