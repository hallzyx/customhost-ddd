using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RoomDevicePreference entity to RoomDevicePreferenceResource
/// </summary>
public static class RoomDevicePreferenceResourceFromEntityAssembler
{
    /// <summary>
    /// Convert RoomDevicePreference entity to RoomDevicePreferenceResource
    /// </summary>
    /// <param name="entity"><see cref="RoomDevicePreference"/> entity to convert</param>
    /// <returns><see cref="RoomDevicePreferenceResource"/> converted from <see cref="RoomDevicePreference"/> entity</returns>
    public static RoomDevicePreferenceResource ToResourceFromEntity(RoomDevicePreference entity)
    {
        return new RoomDevicePreferenceResource(
            entity.Id,
            entity.RoomDeviceId,
            entity.Preferences,
            entity.CreatedAt
        );
    }
}
