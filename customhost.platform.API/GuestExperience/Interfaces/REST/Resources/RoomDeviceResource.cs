namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Room Device resource for API responses
/// </summary>
public record RoomDeviceResource(
    int Id,
    int RoomId,
    int IoTDeviceId,
    string Status,
    DateTime CreatedAt,
    IoTDeviceResource IoTDevice
);
