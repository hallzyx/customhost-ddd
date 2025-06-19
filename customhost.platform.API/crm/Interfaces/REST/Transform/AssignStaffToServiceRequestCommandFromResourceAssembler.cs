using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class AssignStaffToServiceRequestCommandFromResourceAssembler
{    public static AssignStaffToServiceRequestCommand ToCommandFromResource(int id, AssignStaffToServiceRequestResource resource)
    {
        int staffId = resource.AssignedTo ?? 0;
        return new AssignStaffToServiceRequestCommand(id, staffId);
    }
}
