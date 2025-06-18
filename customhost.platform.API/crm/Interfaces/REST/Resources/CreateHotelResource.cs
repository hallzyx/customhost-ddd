namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Create Hotel Resource
/// </summary>
public record CreateHotelResource(
    string Name,
    string Address,
    string Email,
    string Phone,
    int AdminId
);
