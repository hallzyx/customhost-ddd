using System.ComponentModel.DataAnnotations;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record CreateServiceRequestResource
{
    [Required(ErrorMessage = "User ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive integer.")]
    public int? UserId { get; set; }
    
    [Required(ErrorMessage = "Hotel ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive integer.")]
    public int? HotelId { get; set; }
    
    [Required(ErrorMessage = "Room ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Room ID must be a positive integer.")]
    public int? RoomId { get; set; }
    
    [Required(ErrorMessage = "Service request type is required.")]
    public EServiceRequestType? Type { get; set; }
    
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 500 characters.")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Service request status is required.")]
    public EServiceRequestStatus? Status { get; set; }
    
    [Required(ErrorMessage = "Assigned To is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Assigned To must be a positive integer.")]
    public int? AsignedTo { get; set; }
}
