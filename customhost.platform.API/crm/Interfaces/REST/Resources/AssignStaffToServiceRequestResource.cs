using System.ComponentModel.DataAnnotations;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record AssignStaffToServiceRequestResource
{
    [Required(ErrorMessage = "Staff member is required.")]
    public string? AssignedTo { get; set; }
}
