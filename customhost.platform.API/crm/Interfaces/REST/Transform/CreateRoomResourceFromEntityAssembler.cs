using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform room entity to room resource
/// </summary>
public static class RoomResourceFromEntityAssembler
{
    /// <summary>
    /// Transform room entity to room resource
    /// </summary>
    /// <param name="room">Room entity</param>
    /// <returns>Room resource</returns>
    public static RoomResource ToResourceFromEntity(Room room)
    {
        return new RoomResource(
            room.Id,
            room.RoomNumber,
            room.Status.ToString(),
            room.Type.ToString(),
            room.HotelId,
            room.Price,
            room.Floor
        );
    }

    /// <summary>
    /// Transform room entities to room resources
    /// </summary>
    /// <param name="rooms">Room entities</param>
    /// <returns>Room resources</returns>
    public static IEnumerable<RoomResource> ToResourceFromEntity(IEnumerable<Room> rooms)
    {
        return rooms.Select(ToResourceFromEntity);
    }
}