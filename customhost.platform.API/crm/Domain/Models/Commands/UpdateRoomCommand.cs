using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Update Room Command
/// </summary>
/// <param name="Id">Room ID</param>
/// <param name="RoomNumber">Room number</param>
/// <param name="Status">Room status</param>
/// <param name="Type">Room type</param>
/// <param name="HotelId">Hotel ID</param>
/// <param name="Price">Room price per night</param>
/// <param name="Floor">Floor number</param>
public record UpdateRoomCommand(
    int Id,
    int RoomNumber,
    ERoomStatus Status,
    ERoomType Type,
    int HotelId,
    decimal Price,
    int Floor
);
