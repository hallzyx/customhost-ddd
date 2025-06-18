using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert CreateIoTDeviceResource to CreateIoTDeviceCommand
/// </summary>
public static class CreateIoTDeviceCommandFromResourceAssembler
{
    /// <summary>
    /// Convert CreateIoTDeviceResource to CreateIoTDeviceCommand
    /// </summary>
    /// <param name="resource"><see cref="CreateIoTDeviceResource"/> resource to convert</param>
    /// <returns><see cref="CreateIoTDeviceCommand"/> converted from <see cref="CreateIoTDeviceResource"/> resource</returns>
    public static CreateIoTDeviceCommand ToCommandFromResource(CreateIoTDeviceResource resource)
    {
        return new CreateIoTDeviceCommand(
            resource.Name,
            resource.DeviceType,
            resource.ConfigSchema
        );
    }
}
