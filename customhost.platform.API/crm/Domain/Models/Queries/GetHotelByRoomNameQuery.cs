namespace customhost_backend.crm.Domain.Models.Queries;

public record GetHotelByRoomNameQuery
{
    public int RoomName { get; init; }

    public GetHotelByRoomNameQuery(int roomName)
    {
        if(roomName < 100 || roomName > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(roomName), "Room name must be between 100 and 999.");
        }
        RoomName = roomName;
    }
};