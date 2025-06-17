using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Commands;

public class CreateServiceRequestCommand
{
    public int UserId { get; set; }
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public EServiceRequestType Type { get; set; }
    public string Description { get; set; }
    public EServiceRequestStatus Status { get; set; }
    public int AsignedTo { get; set; }
    
    public CreateServiceRequestCommand(int userId, int hotelId, int roomId, EServiceRequestType type, string description, EServiceRequestStatus status, int asignedTo)
    {
        if (userId < 0)
        {
            throw new ArgumentException("User ID must be a positive integer.", nameof(userId));
        }
        if (hotelId < 0)
        {
            throw new ArgumentException("Hotel ID must be a positive integer.", nameof(hotelId));
        }
        if (roomId < 0)
        {
            throw new ArgumentException("Room ID must be a positive integer.", nameof(roomId));
        }
        if (!Enum.IsDefined(typeof(EServiceRequestType), type))
        {
            throw new ArgumentException("Invalid service request type.", nameof(type));
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        }
        if (!Enum.IsDefined(typeof(EServiceRequestStatus), status))
        {
            throw new ArgumentException("Invalid service request status.", nameof(status));
        }
        
        UserId = userId;
        HotelId = hotelId;
        RoomId = roomId;
        Type = type;
        Description = description;
        Status = status;
        AsignedTo = asignedTo;
    }
}