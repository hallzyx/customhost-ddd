namespace customhost.platform.API.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// User Device Preference resource for API responses
/// </summary>
public record UserDevicePreferenceResource(
    int Id,
    int? UserId,
    int DeviceId,
    string CustomName,
    string Overrides,
    DateTime LastUpdated
);
