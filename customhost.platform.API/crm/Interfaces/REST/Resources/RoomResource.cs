namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Room Resource
/// </summary>
public record RoomResource(
    int Id,
    int RoomNumber,
    string Status,
    string Type,
    int HotelId,
    decimal Price,
    int Floor
);