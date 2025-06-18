using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;

namespace customhost_backend.crm.Application.Internal.QueryServices;

/// <summary>
/// Room Query Service Implementation
/// </summary>
public class RoomQueryService(IRoomRepository roomRepository) : IRoomQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await roomRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Room?> GetByIdAsync(int id)
    {
        return await roomRepository.FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Room>> GetByHotelIdAsync(int hotelId)
    {
        return await roomRepository.FindByHotelIdAsync(hotelId);
    }

    /// <inheritdoc />
    public async Task<Room?> GetByRoomNumberAsync(int roomNumber)
    {
        return await roomRepository.FindByRoomNumberAsync(roomNumber);
    }
}