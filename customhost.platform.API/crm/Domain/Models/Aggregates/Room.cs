using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Aggregates;

public class Room
{
    public int Id { get; }
    public int RoomNumber { get; set; }
    public ERoomStatus Status { get; set; }
    
    public ERoomType Type { get; set; }
    public int HotelId { get; set; }

    public Room(){}
    
    public Room(CreateRoomCommand command)
    {
        if (command.RoomNumber < 100 || command.RoomNumber > 999)
            throw new ArgumentOutOfRangeException(nameof(command.RoomNumber), "Room number must be between 100 and 999.");
        
        this.RoomNumber = command.RoomNumber;
        this.Type = command.Type;
        this.HotelId = command.HotelId;
        this.Status = command.Status;
    }
    
}