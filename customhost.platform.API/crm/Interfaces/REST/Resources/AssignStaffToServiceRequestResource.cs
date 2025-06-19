using System.ComponentModel.DataAnnotations;

namespace customhost_backend.crm.Interfaces.REST.Resources;

public record AssignStaffToServiceRequestResource
{
    [Required(ErrorMessage = "Staff member is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Staff member ID must be a positive integer.")]
    public int? AssignedTo { get; set; }
}
