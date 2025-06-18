using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform update room resource to update room command
/// </summary>
public static class UpdateRoomCommandFromResourceAssembler
{
    /// <summary>
    /// Transform update room resource to update room command
    /// </summary>
    /// <param name="id">Room ID</param>
    /// <param name="resource">Update room resource</param>
    /// <returns>Update room command</returns>
    public static UpdateRoomCommand ToCommandFromResource(int id, UpdateRoomResource resource)
    {
        // Parse enums from string
        if (!Enum.TryParse<ERoomStatus>(resource.Status, out var status))
            throw new ArgumentException($"Invalid room status: {resource.Status}");
            
        if (!Enum.TryParse<ERoomType>(resource.Type, out var type))
            throw new ArgumentException($"Invalid room type: {resource.Type}");

        return new UpdateRoomCommand(
            id,
            resource.RoomNumber,
            status,
            type,
            resource.HotelId,
            resource.Price,
            resource.Floor
        );
    }
}
