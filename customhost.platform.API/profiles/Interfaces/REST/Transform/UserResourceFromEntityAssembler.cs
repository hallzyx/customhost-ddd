using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Interfaces.REST.Resources;

namespace customhost_backend.profiles.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(
            user.Id,
            user.HotelId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Phone,
            user.Role.ToString(),
            user.CreatedAt
        );
    }

    public static List<UserResource> ToResourcesFromEntities(IEnumerable<User> users)
    {
        return users.Select(user => ToResourceFromEntity(user)).ToList();
    }
}
