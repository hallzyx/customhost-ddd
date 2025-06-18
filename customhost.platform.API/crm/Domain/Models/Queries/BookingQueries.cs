namespace customhost_backend.crm.Domain.Models.Queries;

public record GetAllBookingsQuery();

public record GetBookingByIdQuery(int BookingId);

public record GetBookingsByUserIdQuery(int UserId);

public record GetBookingsByHotelIdQuery(int HotelId);

public record GetBookingsByRoomIdQuery(int RoomId);

public record GetBookingsByStatusQuery(string Status);

public record GetBookingsByDateRangeQuery(DateTime StartDate, DateTime EndDate);
