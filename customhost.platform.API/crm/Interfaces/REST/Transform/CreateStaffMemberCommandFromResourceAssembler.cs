using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class CreateStaffMemberCommandFromResourceAssembler
{
    public static CreateStaffMemberCommand ToCommandFromResource(CreateStaffMemberResource resource)
    {
        if (!Enum.TryParse<Department>(resource.Department, true, out var department))
        {
            throw new ArgumentException($"Invalid department: {resource.Department}");
        }
        
        return new CreateStaffMemberCommand(
            resource.HotelId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone,
            department
        );
    }
}
