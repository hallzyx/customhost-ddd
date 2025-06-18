namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// IoT Device resource for API responses
/// </summary>
public record IoTDeviceResource(
    int Id,
    string Name,
    string DeviceType,
    string ConfigSchema,
    string Status,
    DateTime CreatedAt
);
