namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Update IoT Device resource for API requests
/// </summary>
public record UpdateIoTDeviceResource(
    string Name,
    string DeviceType,
    string? ConfigSchema = null
);
