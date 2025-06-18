using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Interfaces.REST.Resources;

namespace customhost_backend.profiles.Interfaces.REST.Transform;

public static class UpdateUserCommandFromResourceAssembler
{
    public static UpdateUserCommand ToCommandFromResource(int id, UpdateUserResource resource)
    {
        EUserRole? role = null;
        if (!string.IsNullOrEmpty(resource.Role) && Enum.TryParse<EUserRole>(resource.Role, true, out var parsedRole))
            role = parsedRole;

        return new UpdateUserCommand(
            id,
            resource.HotelId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone,
            role
        );
    }
}
