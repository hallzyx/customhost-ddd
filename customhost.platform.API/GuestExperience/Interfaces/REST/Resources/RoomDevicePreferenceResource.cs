namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Room Device Preference resource for API responses
/// </summary>
public record RoomDevicePreferenceResource(
    int Id,
    int RoomDeviceId,
    string Preferences,
    DateTime CreatedAt
);
