using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class UpdateStaffMemberCommandFromResourceAssembler
{
    public static UpdateStaffMemberCommand ToCommandFromResource(int staffMemberId, UpdateStaffMemberResource resource)
    {
        if (!Enum.TryParse<Department>(resource.Department, true, out var department))
        {
            throw new ArgumentException($"Invalid department: {resource.Department}");
        }
        
        return new UpdateStaffMemberCommand(
            staffMemberId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone,
            department
        );
    }
}
