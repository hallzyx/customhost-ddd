using customhost.platform.API.GuestExperience.Domain.Model.Aggregates;
using customhost.platform.API.GuestExperience.Domain.Model.Commands;

namespace customhost.platform.API.GuestExperience.Domain.Services;

/// <summary>
/// IoT Device command service interface
/// </summary>
public interface IIoTDeviceCommandService
{
    Task<IoTDevice?> Handle(CreateIoTDeviceCommand command);
    Task<IoTDevice?> Handle(UpdateIoTDeviceCommand command);
    Task<bool> Handle(DeleteIoTDeviceCommand command);
}
