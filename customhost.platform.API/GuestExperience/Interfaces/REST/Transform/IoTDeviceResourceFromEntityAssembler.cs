using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert IoTDevice entity to IoTDeviceResource
/// </summary>
public static class IoTDeviceResourceFromEntityAssembler
{
    /// <summary>
    /// Convert IoTDevice entity to IoTDeviceResource
    /// </summary>
    /// <param name="entity"><see cref="IoTDevice"/> entity to convert</param>
    /// <returns><see cref="IoTDeviceResource"/> converted from <see cref="IoTDevice"/> entity</returns>
    public static IoTDeviceResource ToResourceFromEntity(IoTDevice entity)
    {
        return new IoTDeviceResource(
            entity.Id,
            entity.Name,
            entity.DeviceType,
            entity.ConfigSchema,
            entity.Status,
            entity.CreatedAt
        );
    }
}
