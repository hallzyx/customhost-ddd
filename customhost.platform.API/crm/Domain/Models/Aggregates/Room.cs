using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Aggregates;

/// <summary>
/// Room Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Room aggregate root.
/// It contains the properties and methods to manage room information.
/// </remarks>
public class Room
{
    public int Id { get; private set; }
    public int RoomNumber { get; private set; }
    public ERoomStatus Status { get; private set; }
    public ERoomType Type { get; private set; }
    public int HotelId { get; private set; }
    public decimal Price { get; private set; }
    public int Floor { get; private set; }

    // For EF Core
    protected Room() { }
    
    public Room(CreateRoomCommand command)
    {
        if (command.RoomNumber < 100 || command.RoomNumber > 999)
            throw new ArgumentOutOfRangeException(nameof(command.RoomNumber), "Room number must be between 100 and 999.");
        
        RoomNumber = command.RoomNumber;
        Type = command.Type;
        HotelId = command.HotelId;
        Status = command.Status;
        Price = command.Price;
        Floor = command.Floor;
    }

    public void UpdateStatus(ERoomStatus status)
    {
        Status = status;
    }

    public void UpdatePrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        Price = price;
    }

    public void UpdateType(ERoomType type)
    {
        Type = type;
    }

    public void UpdateRoom(UpdateRoomCommand command)
    {
        if (command.RoomNumber < 100 || command.RoomNumber > 999)
            throw new ArgumentOutOfRangeException(nameof(command.RoomNumber), "Room number must be between 100 and 999.");
        if (command.Price < 0)
            throw new ArgumentOutOfRangeException(nameof(command.Price), "Price cannot be negative.");

        RoomNumber = command.RoomNumber;
        Status = command.Status;
        Type = command.Type;
        HotelId = command.HotelId;
        Price = command.Price;
        Floor = command.Floor;
    }

    public bool IsAvailable => Status == ERoomStatus.Available;
}