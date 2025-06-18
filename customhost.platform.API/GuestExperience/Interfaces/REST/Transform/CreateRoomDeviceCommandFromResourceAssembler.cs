using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateRoomDeviceResource to CreateRoomDeviceCommand
/// </summary>
public static class CreateRoomDeviceCommandFromResourceAssembler
{
    /// <summary>
    /// Convert CreateRoomDeviceResource to CreateRoomDeviceCommand
    /// </summary>
    /// <param name="resource"><see cref="CreateRoomDeviceResource"/> resource to convert</param>
    /// <returns><see cref="CreateRoomDeviceCommand"/> converted from <see cref="CreateRoomDeviceResource"/> resource</returns>
    public static CreateRoomDeviceCommand ToCommandFromResource(CreateRoomDeviceResource resource)
    {
        return new CreateRoomDeviceCommand(
            resource.RoomId,
            resource.IoTDeviceId,
            resource.Status
        );
    }
}
