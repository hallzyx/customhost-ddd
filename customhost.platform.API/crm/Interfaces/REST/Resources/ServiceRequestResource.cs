using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record ServiceRequestResource
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public EServiceRequestType Type { get; set; }
    public string Description { get; set; }
    public EServiceRequestStatus Status { get; set; }
    public int AsignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompleteAt { get; set; }

    public ServiceRequestResource(int id, int userId, int hotelId, int roomId, EServiceRequestType type, 
        string description, EServiceRequestStatus status, int asignedTo, DateTime createdAt, DateTime? completeAt)
    {
        if (id < 0)
        {
            throw new ArgumentException("Service Request ID must be a positive integer.", nameof(id));
        }
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
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        }
        if (asignedTo < 0)
        {
            throw new ArgumentException("Assigned To must be a positive integer.", nameof(asignedTo));
        }
        
        Id = id;
        UserId = userId;
        HotelId = hotelId;
        RoomId = roomId;
        Type = type;
        Description = description;
        Status = status;
        AsignedTo = asignedTo;
        CreatedAt = createdAt;
        CompleteAt = completeAt;
    }
}
