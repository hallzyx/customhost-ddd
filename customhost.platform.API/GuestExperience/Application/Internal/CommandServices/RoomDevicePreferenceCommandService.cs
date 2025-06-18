using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.GuestExperience.Application.Internal.CommandServices;

/// <summary>
/// Room Device Preference command service implementation
/// </summary>
public class RoomDevicePreferenceCommandService(
    IRoomDevicePreferenceRepository roomDevicePreferenceRepository,
    IRoomDeviceRepository roomDeviceRepository,
    IUnitOfWork unitOfWork
) : IRoomDevicePreferenceCommandService
{
    public async Task<RoomDevicePreference?> Handle(CreateRoomDevicePreferenceCommand command)
    {
        // Verify Room Device exists
        var roomDevice = await roomDeviceRepository.FindByIdAsync(command.RoomDeviceId);
        if (roomDevice is null)
            throw new Exception($"Room Device with id {command.RoomDeviceId} not found");

        // Check if preference already exists for this room device
        var existingPreference = await roomDevicePreferenceRepository.FindByRoomDeviceIdSingleAsync(command.RoomDeviceId);
        if (existingPreference is not null)
            throw new Exception($"Preference for Room Device {command.RoomDeviceId} already exists");

        var roomDevicePreference = new RoomDevicePreference(command);
        await roomDevicePreferenceRepository.AddAsync(roomDevicePreference);
        await unitOfWork.CompleteAsync();
        return roomDevicePreference;
    }

    public async Task<RoomDevicePreference?> Handle(UpdateRoomDevicePreferenceCommand command)
    {
        var roomDevicePreference = await roomDevicePreferenceRepository.FindByIdAsync(command.Id);
        if (roomDevicePreference is null)
            throw new Exception($"Room Device Preference with id {command.Id} not found");

        // Verify Room Device exists
        var roomDevice = await roomDeviceRepository.FindByIdAsync(command.RoomDeviceId);
        if (roomDevice is null)
            throw new Exception($"Room Device with id {command.RoomDeviceId} not found");

        roomDevicePreference.UpdatePreferences(command.Preferences);
        await unitOfWork.CompleteAsync();
        return roomDevicePreference;
    }
}
