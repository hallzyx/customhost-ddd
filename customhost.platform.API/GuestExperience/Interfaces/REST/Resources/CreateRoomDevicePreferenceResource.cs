namespace customhost.platform.API.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Create Room Device Preference resource for API requests
/// </summary>
public record CreateRoomDevicePreferenceResource(
    int RoomDeviceId,
    string Preferences
);
