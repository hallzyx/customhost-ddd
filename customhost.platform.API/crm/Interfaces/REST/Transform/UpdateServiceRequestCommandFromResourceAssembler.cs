using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class UpdateServiceRequestCommandFromResourceAssembler
{
    public static UpdateServiceRequestCommand ToCommandFromResource(int id, UpdateServiceRequestResource resource)
    {
        EServiceRequestType? type = null;
        if (!string.IsNullOrEmpty(resource.Type) && Enum.TryParse<EServiceRequestType>(resource.Type, out var parsedType))
            type = parsedType;

        EServiceRequestStatus? status = null;
        if (!string.IsNullOrEmpty(resource.Status) && Enum.TryParse<EServiceRequestStatus>(resource.Status, out var parsedStatus))
            status = parsedStatus;        EServiceRequestPriority? priority = null;
        if (!string.IsNullOrEmpty(resource.Priority) && Enum.TryParse<EServiceRequestPriority>(resource.Priority, out var parsedPriority))
            priority = parsedPriority;

        // AssignedTo is now int? directly, no need to parse
        int? assignedTo = resource.AssignedTo;

        return new UpdateServiceRequestCommand(
            id,
            0, // UserId - keeping existing
            0, // HotelId - keeping existing
            0, // RoomId - keeping existing
            type ?? EServiceRequestType.Maintenance,
            resource.Title ?? "", // Using Title as Category
            resource.Description ?? "",
            status ?? EServiceRequestStatus.Open,
            priority ?? EServiceRequestPriority.Normal,
            assignedTo
        );
    }
}
