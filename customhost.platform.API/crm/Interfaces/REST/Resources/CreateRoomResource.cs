using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record CreateRoomResource
{
    [Required(ErrorMessage = "Room number is required.")]
    [Range(101,999, ErrorMessage = "Room number must be between 101 and 999.")]
    public int? RoomNumber { get; set; } 
    
    [Required (ErrorMessage = "Hotel status is required.")]
    // [Range(1,2, ErrorMessage = "Room type null or invalid")]
    public ERoomStatus? Status { get; set; }



    [Required(ErrorMessage = "Type Room is required.")]
    // [Range(1,2, ErrorMessage = "Room type null or invalid.")]
    public ERoomType? Type { get; set; }
    
    [Required(ErrorMessage = "Hotel ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive integer.")]
    public int? HotelId { get; set; }
    
}