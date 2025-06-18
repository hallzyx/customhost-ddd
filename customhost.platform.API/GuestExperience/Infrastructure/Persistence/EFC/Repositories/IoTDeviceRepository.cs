using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// IoT Device repository implementation using Entity Framework Core
/// </summary>
public class IoTDeviceRepository(AppDbContext context) : BaseRepository<IoTDevice>(context), IIoTDeviceRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await Context.Set<IoTDevice>().AnyAsync(d => d.Name == name);
    }

    public async Task<IEnumerable<IoTDevice>> FindByDeviceTypeAsync(string deviceType)
    {
        return await Context.Set<IoTDevice>()
            .Where(d => d.DeviceType == deviceType)
            .ToListAsync();
    }
}
