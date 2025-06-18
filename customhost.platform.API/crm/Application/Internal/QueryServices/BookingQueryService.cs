using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;

namespace customhost_backend.crm.Application.Internal.QueryServices;

/// <summary>
/// Booking query service implementation
/// </summary>
public class BookingQueryService(IBookingRepository bookingRepository) : IBookingQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> Handle(GetAllBookingsQuery query)
    {
        return await bookingRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Booking?> Handle(GetBookingByIdQuery query)
    {
        return await bookingRepository.FindByIdAsync(query.BookingId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> Handle(GetBookingsByUserIdQuery query)
    {
        return await bookingRepository.FindByUserIdAsync(query.UserId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> Handle(GetBookingsByHotelIdQuery query)
    {
        return await bookingRepository.FindByHotelIdAsync(query.HotelId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> Handle(GetBookingsByRoomIdQuery query)
    {
        return await bookingRepository.FindByRoomIdAsync(query.RoomId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> Handle(GetBookingsByStatusQuery query)
    {
        if (Enum.TryParse<BookingStatus>(query.Status, true, out var status))
        {
            return await bookingRepository.FindByStatusAsync(status);
        }
        return new List<Booking>();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Booking>> Handle(GetBookingsByDateRangeQuery query)
    {
        return await bookingRepository.FindByDateRangeAsync(query.StartDate, query.EndDate);
    }
}
