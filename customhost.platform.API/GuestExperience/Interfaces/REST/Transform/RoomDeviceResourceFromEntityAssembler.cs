using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;

namespace customhost_backend.GuestExperience.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert RoomDevice entity to RoomDeviceResource
/// </summary>
public static class RoomDeviceResourceFromEntityAssembler
{
    /// <summary>
    /// Convert RoomDevice entity to RoomDeviceResource
    /// </summary>
    /// <param name="entity"><see cref="RoomDevice"/> entity to convert</param>
    /// <returns><see cref="RoomDeviceResource"/> converted from <see cref="RoomDevice"/> entity</returns>
    public static RoomDeviceResource ToResourceFromEntity(RoomDevice entity)
    {
        var iotDeviceResource = entity.IoTDevice != null 
            ? IoTDeviceResourceFromEntityAssembler.ToResourceFromEntity(entity.IoTDevice)
            : new IoTDeviceResource(0, "", "", "", "", DateTime.MinValue);

        return new RoomDeviceResource(
            entity.Id,
            entity.RoomId,
            entity.IoTDeviceId,
            entity.Status,
            entity.CreatedAt,
            iotDeviceResource
        );
    }
}
