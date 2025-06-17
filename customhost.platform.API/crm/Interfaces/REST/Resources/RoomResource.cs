using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record RoomResource
{
    public int Id { get; set; }
    public ERoomStatus Status { get; set; }
    public int RoomNumber { get; set; }
    public ERoomType Type { get; set; }
    public int HotelId { get; set; }

    public RoomResource(int id, ERoomStatus status, int roomNumber, ERoomType type, int hotelId)
    {
        if(id<0)
        {
            throw new ArgumentException("Room ID must be a positive integer.", nameof(id));
        }
        if(roomNumber<0)
        {
            throw new ArgumentException("Room number must be a positive integer.", nameof(roomNumber));
        }
        Id = id;
        Status = status;
        RoomNumber = roomNumber;
        Type = type;
        HotelId = hotelId;
    }
}