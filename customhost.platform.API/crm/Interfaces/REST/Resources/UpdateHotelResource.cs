namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Update Hotel Resource
/// </summary>
public record UpdateHotelResource(
    string Name,
    string Address,
    string Email,
    string Phone
);
