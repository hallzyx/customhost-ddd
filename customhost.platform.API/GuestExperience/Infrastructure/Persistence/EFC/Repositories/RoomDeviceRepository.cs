using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Room Device repository implementation using Entity Framework Core
/// </summary>
public class RoomDeviceRepository(AppDbContext context) : BaseRepository<RoomDevice>(context), IRoomDeviceRepository
{
    public async Task<IEnumerable<RoomDevice>> FindByRoomIdAsync(int roomId)
    {
        return await Context.Set<RoomDevice>()
            .Include(rd => rd.IoTDevice)
            .Where(rd => rd.RoomId == roomId)
            .ToListAsync();
    }

    public async Task<IEnumerable<RoomDevice>> FindByIoTDeviceIdAsync(int iotDeviceId)
    {
        return await Context.Set<RoomDevice>()
            .Include(rd => rd.IoTDevice)
            .Where(rd => rd.IoTDeviceId == iotDeviceId)
            .ToListAsync();
    }

    public async Task<bool> ExistsDeviceInRoomAsync(int roomId, int iotDeviceId)
    {
        return await Context.Set<RoomDevice>()
            .AnyAsync(rd => rd.RoomId == roomId && rd.IoTDeviceId == iotDeviceId);
    }    public new async Task<RoomDevice?> FindByIdAsync(int id)
    {
        return await Context.Set<RoomDevice>()
            .Include(rd => rd.IoTDevice)
            .FirstOrDefaultAsync(rd => rd.Id == id);
    }

    public new async Task<IEnumerable<RoomDevice>> ListAsync()
    {
        return await Context.Set<RoomDevice>()
            .Include(rd => rd.IoTDevice)
            .ToListAsync();
    }
}
