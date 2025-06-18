using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Application.Internal.CommandServices;

/// <summary>
/// Room Device command service implementation
/// </summary>
public class RoomDeviceCommandService(
    IRoomDeviceRepository roomDeviceRepository,
    IIoTDeviceRepository iotDeviceRepository,
    IUnitOfWork unitOfWork
) : IRoomDeviceCommandService
{
    public async Task<RoomDevice?> Handle(CreateRoomDeviceCommand command)
    {
        // Verify IoT Device exists
        var iotDevice = await iotDeviceRepository.FindByIdAsync(command.IoTDeviceId);
        if (iotDevice is null)
            throw new Exception($"IoT Device with id {command.IoTDeviceId} not found");

        // Check if device is already assigned to this room
        if (await roomDeviceRepository.ExistsDeviceInRoomAsync(command.RoomId, command.IoTDeviceId))
            throw new Exception($"IoT Device {command.IoTDeviceId} is already assigned to room {command.RoomId}");

        var roomDevice = new RoomDevice(command);
        await roomDeviceRepository.AddAsync(roomDevice);
        await unitOfWork.CompleteAsync();
        return roomDevice;
    }

    public async Task<RoomDevice?> Handle(UpdateRoomDeviceCommand command)
    {
        var roomDevice = await roomDeviceRepository.FindByIdAsync(command.Id);
        if (roomDevice is null)
            throw new Exception($"Room Device with id {command.Id} not found");

        // Verify IoT Device exists
        var iotDevice = await iotDeviceRepository.FindByIdAsync(command.IoTDeviceId);
        if (iotDevice is null)
            throw new Exception($"IoT Device with id {command.IoTDeviceId} not found");

        // Check if we're changing the assignment and if the new assignment conflicts
        if ((roomDevice.RoomId != command.RoomId || roomDevice.IoTDeviceId != command.IoTDeviceId) &&
            await roomDeviceRepository.ExistsDeviceInRoomAsync(command.RoomId, command.IoTDeviceId))
            throw new Exception($"IoT Device {command.IoTDeviceId} is already assigned to room {command.RoomId}");

        roomDevice.UpdateStatus(command.Status);
        await unitOfWork.CompleteAsync();
        return roomDevice;
    }

    public async Task<bool> Handle(DeleteRoomDeviceCommand command)
    {
        var roomDevice = await roomDeviceRepository.FindByIdAsync(command.Id);
        if (roomDevice is null)
            throw new Exception($"Room Device with id {command.Id} not found");

        roomDeviceRepository.Remove(roomDevice);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
