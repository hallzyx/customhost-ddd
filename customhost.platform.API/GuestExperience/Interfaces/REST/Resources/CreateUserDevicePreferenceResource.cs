namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Create User Device Preference resource for API requests
/// </summary>
public record CreateUserDevicePreferenceResource(
    int? UserId,
    int DeviceId,
    string CustomName,
    string Overrides
);
