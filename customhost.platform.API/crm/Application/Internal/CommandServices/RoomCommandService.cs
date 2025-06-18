using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.CommandServices;

/// <summary>
/// Room Command Service Implementation
/// </summary>
public class RoomCommandService(
    IRoomRepository roomRepository, 
    IUnitOfWork unitOfWork)
    : IRoomCommandService
{
    /// <inheritdoc />
    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        // Check if room number already exists in the hotel
        if (await roomRepository.ExistsByRoomNumberAndHotelIdAsync(command.RoomNumber, command.HotelId))
        {
            return null; // Room already exists
        }

        try
        {
            var room = new Room(command);
            await roomRepository.AddAsync(room);
            await unitOfWork.CompleteAsync();
            return room;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Room?> Handle(UpdateRoomCommand command)
    {
        try
        {
            var room = await roomRepository.FindByIdAsync(command.Id);
            if (room == null) return null;

            // Check if the new room number conflicts with existing rooms (excluding current room)
            if (await roomRepository.ExistsByRoomNumberAndHotelIdAsync(command.RoomNumber, command.HotelId, command.Id))
            {
                return null; // Room number conflict
            }

            room.UpdateRoom(command);
            roomRepository.Update(room);
            await unitOfWork.CompleteAsync();
            return room;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteRoomCommand command)
    {
        try
        {
            var room = await roomRepository.FindByIdAsync(command.Id);
            if (room == null) return false;

            roomRepository.Remove(room);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}