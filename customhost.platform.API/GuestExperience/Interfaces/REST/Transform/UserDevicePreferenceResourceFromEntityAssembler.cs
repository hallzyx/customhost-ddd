using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert UserDevicePreference entity to UserDevicePreferenceResource
/// </summary>
public static class UserDevicePreferenceResourceFromEntityAssembler
{
    /// <summary>
    /// Convert UserDevicePreference entity to UserDevicePreferenceResource
    /// </summary>
    /// <param name="entity"><see cref="UserDevicePreference"/> entity to convert</param>
    /// <returns><see cref="UserDevicePreferenceResource"/> converted from <see cref="UserDevicePreference"/> entity</returns>
    public static UserDevicePreferenceResource ToResourceFromEntity(UserDevicePreference entity)
    {
        return new UserDevicePreferenceResource(
            entity.Id,
            entity.UserId,
            entity.DeviceId,
            entity.CustomName,
            entity.Overrides,
            entity.LastUpdated
        );
    }
}
