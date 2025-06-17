using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class CreateRoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Room room)
    {
        return new RoomResource(
            room.Id,
            room.Status,
            room.RoomNumber,
            room.Type,
            room.HotelId
        );
    }

    public static List<RoomResource> ToResourcesFromEntities(IEnumerable<Room> rooms)
    {
        return rooms.Select(room => ToResourceFromEntity(room)).ToList();
    }
}