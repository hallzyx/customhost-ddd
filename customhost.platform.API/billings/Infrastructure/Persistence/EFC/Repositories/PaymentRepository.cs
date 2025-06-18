using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.ValueObjects;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.billings.Infrastructure.Persistence.EFC.Repositories;

public class PaymentRepository(AppDbContext context) 
    : BaseRepository<Payment>(context), IPaymentRepository
{
    public async Task<IEnumerable<Payment>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Payment>()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> FindByHotelIdAsync(int hotelId)
    {
        return await Context.Set<Payment>()
            .Where(p => p.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Payment>> FindByStatusAsync(EPaymentStatus status)
    {
        return await Context.Set<Payment>()
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<Payment?> FindByBookingIdAsync(int bookingId)
    {
        return await Context.Set<Payment>()
            .FirstOrDefaultAsync(p => p.BookingId == bookingId);
    }
}
