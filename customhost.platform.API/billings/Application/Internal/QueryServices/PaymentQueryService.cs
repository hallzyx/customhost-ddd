using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.ValueObjects;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.billings.Domain.Services;

namespace customhost_backend.billings.Application.Internal.QueryServices;

/// <summary>
/// Payment Query Service Implementation
/// </summary>
public class PaymentQueryService(IPaymentRepository paymentRepository) 
    : IPaymentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Payment>> GetAllAsync()
    {
        return await paymentRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Payment?> GetByIdAsync(int id)
    {
        return await paymentRepository.FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Payment>> GetByUserIdAsync(int userId)
    {
        return await paymentRepository.FindByUserIdAsync(userId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Payment>> GetByHotelIdAsync(int hotelId)
    {
        return await paymentRepository.FindByHotelIdAsync(hotelId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Payment>> GetByStatusAsync(EPaymentStatus status)
    {
        return await paymentRepository.FindByStatusAsync(status);
    }

    /// <inheritdoc />
    public async Task<Payment?> GetByBookingIdAsync(int bookingId)
    {
        return await paymentRepository.FindByBookingIdAsync(bookingId);
    }
}
