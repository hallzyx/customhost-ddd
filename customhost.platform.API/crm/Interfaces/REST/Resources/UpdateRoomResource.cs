using System.ComponentModel.DataAnnotations;

namespace customhost_backend.crm.Interfaces.REST.Resources;

/// <summary>
/// Update Room Resource
/// </summary>
public record UpdateRoomResource
{
    [Required(ErrorMessage = "Room number is required.")]
    [Range(100, 999, ErrorMessage = "Room number must be between 100 and 999.")]
    public int RoomNumber { get; set; }
    
    [Required(ErrorMessage = "Room status is required.")]
    public string Status { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Room type is required.")]
    public string Type { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Hotel ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive integer.")]
    public int HotelId { get; set; }
    
    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Floor is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Floor must be a positive integer.")]
    public int Floor { get; set; }
}
