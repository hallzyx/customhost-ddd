using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Services;

/// <summary>
/// IoT Device command service interface
/// </summary>
public interface IIoTDeviceCommandService
{
    Task<IoTDevice?> Handle(CreateIoTDeviceCommand command);
    Task<IoTDevice?> Handle(UpdateIoTDeviceCommand command);
    Task<bool> Handle(DeleteIoTDeviceCommand command);
}
