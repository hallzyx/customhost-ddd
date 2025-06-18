using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Repositories;

/// <summary>
/// Room Repository Implementation
/// </summary>
public class RoomRepository(AppDbContext context)
    : BaseRepository<Room>(context), IRoomRepository
{
    /// <inheritdoc />
    public async Task<Room?> FindByRoomNumberAsync(int roomNumber)
    {
        return await Context.Set<Room>().FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Room>> FindByHotelIdAsync(int hotelId)
    {
        return await Context.Set<Room>()
            .Where(r => r.HotelId == hotelId)
            .OrderBy(r => r.RoomNumber)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByRoomNumberAndHotelIdAsync(int roomNumber, int hotelId)
    {
        return await Context.Set<Room>()
            .AnyAsync(r => r.RoomNumber == roomNumber && r.HotelId == hotelId);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByRoomNumberAndHotelIdAsync(int roomNumber, int hotelId, int excludeRoomId)
    {
        return await Context.Set<Room>()
            .AnyAsync(r => r.RoomNumber == roomNumber && r.HotelId == hotelId && r.Id != excludeRoomId);
    }
}