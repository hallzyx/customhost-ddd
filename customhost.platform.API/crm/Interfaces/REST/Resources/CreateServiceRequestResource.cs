using System.ComponentModel.DataAnnotations;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record CreateServiceRequestResource
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
    public string? Title { get; set; }
    
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 1000 characters.")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Type is required.")]
    public string? Type { get; set; }
    
    [Required(ErrorMessage = "Priority is required.")]
    public string? Priority { get; set; }
    
    [Required(ErrorMessage = "User ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive integer.")]
    public int? UserId { get; set; }
      [Required(ErrorMessage = "Hotel ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive integer.")]
    public int? HotelId { get; set; }
    
    [Required(ErrorMessage = "Room ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Room ID must be a positive integer.")]
    public int? RoomId { get; set; }
}
