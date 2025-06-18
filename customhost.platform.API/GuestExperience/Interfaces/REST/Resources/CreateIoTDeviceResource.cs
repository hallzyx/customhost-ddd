namespace customhost.platform.API.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Create IoT Device resource for API requests
/// </summary>
public record CreateIoTDeviceResource(
    string Name,
    string DeviceType,
    string? ConfigSchema = null
);
