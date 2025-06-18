using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Booking repository implementation using Entity Framework Core
/// </summary>
public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Booking>()
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> FindByHotelIdAsync(int hotelId)
    {
        return await Context.Set<Booking>()
            .Where(b => b.HotelId == hotelId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> FindByRoomIdAsync(int roomId)
    {
        return await Context.Set<Booking>()
            .Where(b => b.RoomId == roomId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> FindByStatusAsync(BookingStatus status)
    {
        return await Context.Set<Booking>()
            .Where(b => b.Status == status)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> FindByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await Context.Set<Booking>()
            .Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate)
            .OrderBy(b => b.CheckInDate)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkInDate, DateTime checkOutDate, int? excludeBookingId = null)
    {
        var query = Context.Set<Booking>()
            .Where(b => b.RoomId == roomId && 
                       b.Status != BookingStatus.Cancelled && 
                       b.Status != BookingStatus.NoShow &&
                       ((b.CheckInDate < checkOutDate && b.CheckOutDate > checkInDate)));

        if (excludeBookingId.HasValue)
        {
            query = query.Where(b => b.Id != excludeBookingId.Value);
        }

        return !await query.AnyAsync();
    }
}
