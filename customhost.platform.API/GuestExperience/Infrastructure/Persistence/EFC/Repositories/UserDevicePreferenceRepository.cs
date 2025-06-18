using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// User Device Preference repository implementation using Entity Framework Core
/// </summary>
public class UserDevicePreferenceRepository(AppDbContext context) : BaseRepository<UserDevicePreference>(context), IUserDevicePreferenceRepository
{    public async Task<IEnumerable<UserDevicePreference>> FindByUserIdAsync(int? userId)
    {
        return await Context.Set<UserDevicePreference>()
            .Where(udp => udp.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserDevicePreference>> FindByDeviceIdAsync(int deviceId)
    {
        return await Context.Set<UserDevicePreference>()
            .Where(udp => udp.DeviceId == deviceId)
            .ToListAsync();
    }    public async Task<UserDevicePreference?> FindByUserIdAndDeviceIdAsync(int? userId, int deviceId)
    {
        return await Context.Set<UserDevicePreference>()
            .FirstOrDefaultAsync(udp => udp.UserId == userId && udp.DeviceId == deviceId);
    }
}
