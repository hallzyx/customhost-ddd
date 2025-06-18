using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Queries;

namespace customhost.platform.API.GuestExperience.Domain.Services;

/// <summary>
/// IoT Device query service interface
/// </summary>
public interface IIoTDeviceQueryService
{
    Task<IEnumerable<IoTDevice>> Handle(GetAllIoTDevicesQuery query);
    Task<IoTDevice?> Handle(GetIoTDeviceByIdQuery query);
}
