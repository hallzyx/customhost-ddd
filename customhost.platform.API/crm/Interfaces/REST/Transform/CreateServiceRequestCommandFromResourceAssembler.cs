using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class CreateServiceRequestCommandFromResourceAssembler
{    public static CreateServiceRequestCommand ToCommandFromResource(CreateServiceRequestResource resource)
    {
        return new CreateServiceRequestCommand(
            resource.UserId!.Value,
            resource.HotelId!.Value,
            resource.RoomId!.Value,
            resource.Type!.Value,
            resource.Description!,
            resource.Status!.Value,
            resource.AsignedTo!.Value
        );
    }
}
