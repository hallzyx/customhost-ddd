using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Application.Internal.CommandServices;

/// <summary>
/// IoT Device command service implementation
/// </summary>
public class IoTDeviceCommandService(
    IIoTDeviceRepository iotDeviceRepository,
    IUnitOfWork unitOfWork
) : IIoTDeviceCommandService
{
    public async Task<IoTDevice?> Handle(CreateIoTDeviceCommand command)
    {
        if (await iotDeviceRepository.ExistsByNameAsync(command.Name))
            throw new Exception($"IoT Device with name {command.Name} already exists");

        var iotDevice = new IoTDevice(command);
        await iotDeviceRepository.AddAsync(iotDevice);
        await unitOfWork.CompleteAsync();
        return iotDevice;
    }

    public async Task<IoTDevice?> Handle(UpdateIoTDeviceCommand command)
    {
        var iotDevice = await iotDeviceRepository.FindByIdAsync(command.Id);
        if (iotDevice is null) 
            throw new Exception($"IoT Device with id {command.Id} not found");

        // Check if name is being changed and if new name already exists
        if (iotDevice.Name != command.Name && await iotDeviceRepository.ExistsByNameAsync(command.Name))
            throw new Exception($"IoT Device with name {command.Name} already exists");

        iotDevice.UpdateDevice(command.Name, command.DeviceType, command.ConfigSchema);
        await unitOfWork.CompleteAsync();
        return iotDevice;
    }

    public async Task<bool> Handle(DeleteIoTDeviceCommand command)
    {
        var iotDevice = await iotDeviceRepository.FindByIdAsync(command.Id);
        if (iotDevice is null) 
            throw new Exception($"IoT Device with id {command.Id} not found");

        iotDeviceRepository.Remove(iotDevice);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
