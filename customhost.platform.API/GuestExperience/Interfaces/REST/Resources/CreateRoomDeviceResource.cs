namespace customhost_backend.GuestExperience.Interfaces.REST.Resources;

/// <summary>
/// Create Room Device resource for API requests
/// </summary>
public record CreateRoomDeviceResource(
    int RoomId,
    int IoTDeviceId,
    string Status = "working"
);
