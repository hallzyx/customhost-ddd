using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Queries;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// IoT Device query service interface
/// </summary>
public interface IIoTDeviceQueryService
{
    Task<IEnumerable<IoTDevice>> Handle(GetAllIoTDevicesQuery query);
    Task<IoTDevice?> Handle(GetIoTDeviceByIdQuery query);
}
