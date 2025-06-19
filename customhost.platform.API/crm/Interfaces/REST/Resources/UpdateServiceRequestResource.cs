using System.ComponentModel.DataAnnotations;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record UpdateServiceRequestResource
{
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
    public string? Title { get; set; }
    
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 1000 characters.")]
    public string? Description { get; set; }
    
    public string? Type { get; set; }
      public string? Status { get; set; }
    
    public string? Priority { get; set; }
    
    public int? AssignedTo { get; set; }
}
