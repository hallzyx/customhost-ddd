using customhost.platform.API.GuestExperience.Domain.Model.Commands;
using customhost.platform.API.GuestExperience.Interfaces.REST.Resources;

namespace customhost.platform.API.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert UpdateIoTDeviceResource to UpdateIoTDeviceCommand
/// </summary>
public static class UpdateIoTDeviceCommandFromResourceAssembler
{
    /// <summary>
    /// Convert UpdateIoTDeviceResource to UpdateIoTDeviceCommand
    /// </summary>
    /// <param name="id">The ID of the IoT device to update</param>
    /// <param name="resource"><see cref="UpdateIoTDeviceResource"/> resource to convert</param>
    /// <returns><see cref="UpdateIoTDeviceCommand"/> converted from <see cref="UpdateIoTDeviceResource"/> resource</returns>
    public static UpdateIoTDeviceCommand ToCommandFromResource(int id, UpdateIoTDeviceResource resource)
    {
        return new UpdateIoTDeviceCommand(
            id,
            resource.Name,
            resource.DeviceType,
            resource.ConfigSchema
        );
    }
}
