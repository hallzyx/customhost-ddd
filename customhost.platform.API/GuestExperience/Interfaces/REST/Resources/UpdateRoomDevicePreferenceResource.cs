namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Update Room Device Preference resource for API requests
/// </summary>
public record UpdateRoomDevicePreferenceResource(
    int RoomDeviceId,
    string Preferences
);
