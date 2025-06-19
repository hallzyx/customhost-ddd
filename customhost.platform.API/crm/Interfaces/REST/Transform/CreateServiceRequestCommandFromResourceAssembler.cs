using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class CreateServiceRequestCommandFromResourceAssembler
{
    public static CreateServiceRequestCommand ToCommandFromResource(CreateServiceRequestResource resource)
    {
        if (!Enum.TryParse<EServiceRequestType>(resource.Type, out var type))
            type = EServiceRequestType.Maintenance;
              if (!Enum.TryParse<EServiceRequestPriority>(resource.Priority, out var priority))
            priority = EServiceRequestPriority.Normal;        return new CreateServiceRequestCommand(
            resource.UserId ?? 0,
            resource.HotelId ?? 0,
            resource.RoomId ?? 0,
            type,
            resource.Title ?? "", // Using Title as Category
            resource.Description ?? "",
            EServiceRequestStatus.Open, // Default status
            priority,
            null // AssignedTo - not provided in creation
        );
    }
}
