using customhost.platform.API.GuestExperience.Domain.Model.Commands;
using customhost.platform.API.GuestExperience.Interfaces.REST.Resources;

namespace customhost.platform.API.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateRoomDevicePreferenceResource to CreateRoomDevicePreferenceCommand
/// </summary>
public static class CreateRoomDevicePreferenceCommandFromResourceAssembler
{
    /// <summary>
    /// Convert CreateRoomDevicePreferenceResource to CreateRoomDevicePreferenceCommand
    /// </summary>
    /// <param name="resource"><see cref="CreateRoomDevicePreferenceResource"/> resource to convert</param>
    /// <returns><see cref="CreateRoomDevicePreferenceCommand"/> converted from <see cref="CreateRoomDevicePreferenceResource"/> resource</returns>
    public static CreateRoomDevicePreferenceCommand ToCommandFromResource(CreateRoomDevicePreferenceResource resource)
    {
        return new CreateRoomDevicePreferenceCommand(
            resource.RoomDeviceId,
            resource.Preferences
        );
    }
}
