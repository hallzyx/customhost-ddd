using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class ChangeStaffMemberStatusCommandFromResourceAssembler
{
    public static ChangeStaffMemberStatusCommand ToCommandFromResource(int staffMemberId, ChangeStaffMemberStatusResource resource)
    {
        if (!Enum.TryParse<StaffStatus>(resource.Status, true, out var status))
        {
            throw new ArgumentException($"Invalid status: {resource.Status}");
        }
        
        return new ChangeStaffMemberStatusCommand(staffMemberId, status);
    }
}
