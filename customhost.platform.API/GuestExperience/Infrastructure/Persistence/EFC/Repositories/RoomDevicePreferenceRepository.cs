using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Room Device Preference repository implementation using Entity Framework Core
/// </summary>
public class RoomDevicePreferenceRepository(AppDbContext context) : BaseRepository<RoomDevicePreference>(context), IRoomDevicePreferenceRepository
{
    public async Task<IEnumerable<RoomDevicePreference>> FindByRoomDeviceIdAsync(int roomDeviceId)
    {
        return await Context.Set<RoomDevicePreference>()
            .Where(rdp => rdp.RoomDeviceId == roomDeviceId)
            .ToListAsync();
    }

    public async Task<RoomDevicePreference?> FindByRoomDeviceIdSingleAsync(int roomDeviceId)
    {
        return await Context.Set<RoomDevicePreference>()
            .FirstOrDefaultAsync(rdp => rdp.RoomDeviceId == roomDeviceId);
    }
}
