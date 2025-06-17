using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Services;

public interface IRoomCommandService
{
    Task<Room?> Handle(CreateRoomCommand command);
}