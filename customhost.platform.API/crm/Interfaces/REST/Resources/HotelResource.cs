namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Hotel Resource
/// </summary>
public record HotelResource(
    int Id,
    string Name,
    string Address,
    string Email,
    string Phone,
    string Status,
    DateTime CreatedAt,
    int AdminId
);
