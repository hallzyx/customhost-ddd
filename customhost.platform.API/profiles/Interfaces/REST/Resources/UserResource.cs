namespace customhost_backend.profiles.Interfaces.REST.Resources;

public record UserResource(
    int Id,
    int HotelId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Role,
    DateTime CreatedAt
);
