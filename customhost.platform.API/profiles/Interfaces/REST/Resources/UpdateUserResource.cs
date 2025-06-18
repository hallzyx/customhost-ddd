using System.ComponentModel.DataAnnotations;

namespace customhost_backend.profiles.Interfaces.REST.Resources;

public record UpdateUserResource
{
    [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive integer.")]
    public int? HotelId { get; set; }
    
    [StringLength(100, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 100 characters.")]
    public string? FirstName { get; set; }
    
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 100 characters.")]
    public string? LastName { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
    public string? Email { get; set; }
    
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone must be between 1 and 20 characters.")]
    public string? Phone { get; set; }
    
    public string? Role { get; set; }
}
