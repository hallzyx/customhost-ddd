using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Interfaces.REST.Resources;

namespace customhost_backend.profiles.Interfaces.REST.Transform;

public static class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        if (!Enum.TryParse<EUserRole>(resource.Role, true, out var role))
            role = EUserRole.Guest;

        return new CreateUserCommand(
            resource.HotelId ?? 0,
            resource.FirstName ?? "",
            resource.LastName ?? "",
            resource.Email ?? "",
            resource.Password ?? "",
            resource.Phone ?? "",
            role
        );
    }
}
