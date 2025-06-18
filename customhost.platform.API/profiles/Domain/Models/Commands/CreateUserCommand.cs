using customhost_backend.profiles.Domain.Models.ValueObjects;

namespace customhost_backend.profiles.Domain.Models.Commands;

public record CreateUserCommand(
    int HotelId,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    EUserRole Role
);
