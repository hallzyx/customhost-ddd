namespace customhost.platform.API.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Update Room Device resource for API requests
/// </summary>
public record UpdateRoomDeviceResource(
    int RoomId,
    int IoTDeviceId,
    string Status
);
