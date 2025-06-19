using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Room with IoT Devices Resource
/// </summary>
public record RoomWithDevicesResource(
    int Id,
    int RoomNumber,
    string Status,
    string Type,
    int HotelId,
    decimal Price,
    int Floor,
    IEnumerable<RoomDeviceResource> Devices
);
