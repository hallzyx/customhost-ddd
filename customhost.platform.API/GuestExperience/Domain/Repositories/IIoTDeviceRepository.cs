using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Domain.Repositories;

/// <summary>
/// Repository interface for IoT Device aggregate
/// </summary>
public interface IIoTDeviceRepository : IBaseRepository<IoTDevice>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<IEnumerable<IoTDevice>> FindByDeviceTypeAsync(string deviceType);
}
