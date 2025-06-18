using customhost_backend.profiles.Domain.Models.ValueObjects;

namespace customhost_backend.profiles.Domain.Models.Commands;

public record UpdateUserCommand(
    int Id,
    int? HotelId,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    EUserRole? Role
);
