using System.ComponentModel.DataAnnotations;

namespace customhost_backend.profiles.Interfaces.REST.Resources;

public record CreateUserResource
{
    [Required(ErrorMessage = "Hotel ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hotel ID must be a positive integer.")]
    public int? HotelId { get; set; }
    
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 100 characters.")]
    public string? FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Last name must be between 1 and 100 characters.")]
    public string? LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "Phone is required.")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Phone must be between 1 and 20 characters.")]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Role is required.")]
    public string? Role { get; set; }
}
