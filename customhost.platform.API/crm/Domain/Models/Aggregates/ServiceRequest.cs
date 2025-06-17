using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Aggregates;

public class ServiceRequest
{
    public int Id { get;  }
    public int UserId { get; set; }
    public int HotelId { get; set; }
    public int RoomId { get; set; }
    public EServiceRequestType Type { get; set; }
    public string Description { get; set; }
    public EServiceRequestStatus Status { get; set; }
    public int AsignedTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompleteAt { get; set; }
    
    public ServiceRequest(){}

    public ServiceRequest(CreateServiceRequestCommand command)
    {
        this.UserId = command.UserId;
        this.HotelId = command.HotelId;
        this.RoomId = command.RoomId;
        this.Type = command.Type;
        this.Description = command.Description;
        this.Status = command.Status;
        this.AsignedTo = command.AsignedTo;
        this.CreatedAt = new DateTime();
        this.CompleteAt = null;
    }
    
}