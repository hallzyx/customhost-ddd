using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.ValueObjects;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.billings.Domain.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    Task<IEnumerable<Payment>> FindByUserIdAsync(int userId);
    Task<IEnumerable<Payment>> FindByHotelIdAsync(int hotelId);
    Task<IEnumerable<Payment>> FindByStatusAsync(EPaymentStatus status);
    Task<Payment?> FindByBookingIdAsync(int bookingId);
}
