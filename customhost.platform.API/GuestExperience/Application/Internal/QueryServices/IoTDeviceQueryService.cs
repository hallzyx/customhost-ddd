using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Queries;
using customhost.platform.API.GuestExperience.Domain.Repositories;
using customhost.platform.API.GuestExperience.Domain.Services;

namespace customhost.platform.API.GuestExperience.Application.Internal.QueryServices;

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
