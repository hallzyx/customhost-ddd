using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Commands;

public record CreateRoomCommand
{
    public ERoomStatus Status { get; set; }
    public int RoomNumber { get; set; }
    public ERoomType Type { get; set; }
    public int HotelId { get; set; }

    public CreateRoomCommand(int roomNumber, ERoomStatus status, ERoomType type, int hotelId)
    {
        if (roomNumber < 0)
        {
            throw new ArgumentException("Room number must be a positive integer.", nameof(roomNumber));
        }
        if (hotelId < 0)
        {
            throw new ArgumentException("Hotel ID must be a positive integer.", nameof(hotelId));
        }
        if (!Enum.IsDefined(typeof(ERoomType), type))
        {
            throw new ArgumentException("Invalid room type.", nameof(type));
        }
        if (!Enum.IsDefined(typeof(ERoomStatus), status))
        {
            throw new ArgumentException("Invalid room status.", nameof(status));
        }
        
        RoomNumber = roomNumber;
        Status = status;
        Type = type;
        HotelId = hotelId;
        
    }
};