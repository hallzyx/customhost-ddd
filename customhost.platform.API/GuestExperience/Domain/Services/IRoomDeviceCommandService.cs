using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// Room Device command service interface
/// </summary>
public interface IRoomDeviceCommandService
{
    Task<RoomDevice?> Handle(CreateRoomDeviceCommand command);
    Task<RoomDevice?> Handle(UpdateRoomDeviceCommand command);
    Task<bool> Handle(DeleteRoomDeviceCommand command);
}
