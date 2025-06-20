using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost.platform.API.GuestExperience.Domain.Repositories;

/// <summary>
/// Repository interface for Room Device aggregate
/// </summary>
public interface IRoomDeviceRepository : IBaseRepository<RoomDevice>
{
    Task<IEnumerable<RoomDevice>> FindByRoomIdAsync(int roomId);
    Task<IEnumerable<RoomDevice>> FindByIoTDeviceIdAsync(int iotDeviceId);
    Task<bool> ExistsDeviceInRoomAsync(int roomId, int iotDeviceId);
}
