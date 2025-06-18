using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Application.Internal.CommandServices;

/// <summary>
/// User Device Preference command service implementation
/// </summary>
public class UserDevicePreferenceCommandService(
    IUserDevicePreferenceRepository userDevicePreferenceRepository,
    IIoTDeviceRepository iotDeviceRepository,
    IUnitOfWork unitOfWork
) : IUserDevicePreferenceCommandService
{
    public async Task<UserDevicePreference?> Handle(CreateUserDevicePreferenceCommand command)
    {
        // Verify IoT Device exists
        var iotDevice = await iotDeviceRepository.FindByIdAsync(command.DeviceId);
        if (iotDevice is null)
            throw new Exception($"Device with id {command.DeviceId} not found");

        // Check if user already has preference for this device
        var existingPreference = await userDevicePreferenceRepository.FindByUserIdAndDeviceIdAsync(command.UserId, command.DeviceId);
        if (existingPreference is not null)
            throw new Exception($"User {command.UserId} already has preferences for device {command.DeviceId}");

        var userDevicePreference = new UserDevicePreference(command);
        await userDevicePreferenceRepository.AddAsync(userDevicePreference);
        await unitOfWork.CompleteAsync();
        return userDevicePreference;
    }

    public async Task<UserDevicePreference?> Handle(UpdateUserDevicePreferenceCommand command)
    {
        var userDevicePreference = await userDevicePreferenceRepository.FindByIdAsync(command.Id);
        if (userDevicePreference is null)
            throw new Exception($"User Device Preference with id {command.Id} not found");

        // Verify IoT Device exists
        var iotDevice = await iotDeviceRepository.FindByIdAsync(command.DeviceId);
        if (iotDevice is null)
            throw new Exception($"Device with id {command.DeviceId} not found");

        userDevicePreference.UpdatePreference(command.CustomName, command.Overrides);
        await unitOfWork.CompleteAsync();
        return userDevicePreference;
    }

    public async Task<bool> Handle(DeleteUserDevicePreferenceCommand command)
    {
        var userDevicePreference = await userDevicePreferenceRepository.FindByIdAsync(command.Id);
        if (userDevicePreference is null)
            throw new Exception($"User Device Preference with id {command.Id} not found");

        userDevicePreferenceRepository.Remove(userDevicePreference);
        await unitOfWork.CompleteAsync();
        return true;
    }
}
