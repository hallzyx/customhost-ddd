using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class CreateRoomCommandFromResourceAssembler
{
    public static CreateRoomCommand ToCommandFromResource
        (CreateRoomResource resource)
    {
       return new CreateRoomCommand(
            resource.RoomNumber.Value,
            resource.Status.Value,
            resource.Type.Value,
            resource.HotelId.Value
        );
    }
}