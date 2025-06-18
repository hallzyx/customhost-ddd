using System.ComponentModel.DataAnnotations;

namespace customhost_backend.billings.Interfaces.REST.Resources;

public record CreatePaymentResource
{
    public int? BookingId { get; set; }
    
    [Required(ErrorMessage = "User ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive integer.")]
    public int? UserId { get; set; }
    
    [Required(ErrorMessage = "Hotel ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive integer.")]
    public int? HotelId { get; set; }
    
    [Required(ErrorMessage = "Room ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Room ID must be a positive integer.")]
    public int? RoomId { get; set; }
    
    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal? Amount { get; set; }
    
    [Required(ErrorMessage = "Currency is required.")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be 3 characters.")]
    public string? Currency { get; set; }
    
    public DateTime? CheckInDate { get; set; }
    
    public DateTime? CheckOutDate { get; set; }
    
    [Required(ErrorMessage = "Payment method is required.")]
    public string? PaymentMethod { get; set; }
    
    public string? Status { get; set; }
}
