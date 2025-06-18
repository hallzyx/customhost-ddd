using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class AssignStaffToServiceRequestCommandFromResourceAssembler
{
    public static AssignStaffToServiceRequestCommand ToCommandFromResource(int id, AssignStaffToServiceRequestResource resource)
    {
        int staffId = 0;
        if (!string.IsNullOrEmpty(resource.AssignedTo) && int.TryParse(resource.AssignedTo, out var parsedStaffId))
            staffId = parsedStaffId;

        return new AssignStaffToServiceRequestCommand(id, staffId);
    }
}
