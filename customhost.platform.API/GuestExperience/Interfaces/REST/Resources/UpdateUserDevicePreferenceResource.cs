namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Update User Device Preference resource for API requests
/// </summary>
public record UpdateUserDevicePreferenceResource(
    int? UserId,
    int DeviceId,
    string CustomName,
    string Overrides
);
