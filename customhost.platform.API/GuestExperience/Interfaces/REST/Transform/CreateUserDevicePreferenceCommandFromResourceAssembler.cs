using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateUserDevicePreferenceResource to CreateUserDevicePreferenceCommand
/// </summary>
public static class CreateUserDevicePreferenceCommandFromResourceAssembler
{
    /// <summary>
    /// Convert CreateUserDevicePreferenceResource to CreateUserDevicePreferenceCommand
    /// </summary>
    /// <param name="resource"><see cref="CreateUserDevicePreferenceResource"/> resource to convert</param>
    /// <returns><see cref="CreateUserDevicePreferenceCommand"/> converted from <see cref="CreateUserDevicePreferenceResource"/> resource</returns>
    public static CreateUserDevicePreferenceCommand ToCommandFromResource(CreateUserDevicePreferenceResource resource)
    {
        return new CreateUserDevicePreferenceCommand(
            resource.UserId,
            resource.DeviceId,
            resource.CustomName,
            resource.Overrides
        );
    }
}
