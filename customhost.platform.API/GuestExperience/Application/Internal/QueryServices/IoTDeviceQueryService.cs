using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;

namespace customhost_backend.GuestExperience.Application.Internal.QueryServices;

/// <summary>
/// IoT Device query service implementation
/// </summary>
public class IoTDeviceQueryService(IIoTDeviceRepository iotDeviceRepository) : IIoTDeviceQueryService
{
    public async Task<IEnumerable<IoTDevice>> Handle(GetAllIoTDevicesQuery query)
    {
        return await iotDeviceRepository.ListAsync();
    }

    public async Task<IoTDevice?> Handle(GetIoTDeviceByIdQuery query)
    {
        return await iotDeviceRepository.FindByIdAsync(query.Id);
    }
}
