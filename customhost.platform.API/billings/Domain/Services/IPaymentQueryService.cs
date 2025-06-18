using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.ValueObjects;

namespace customhost_backend.billings.Domain.Services;

/// <summary>
/// Payment Query Service Interface
/// </summary>
public interface IPaymentQueryService
{
    /// <summary>
    /// Get all payments
    /// </summary>
    /// <returns>All payments</returns>
    Task<IEnumerable<Payment>> GetAllAsync();
    
    /// <summary>
    /// Get payment by ID
    /// </summary>
    /// <param name="id">Payment ID</param>
    /// <returns>Payment or null if not found</returns>
    Task<Payment?> GetByIdAsync(int id);
    
    /// <summary>
    /// Get payments by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User payments</returns>
    Task<IEnumerable<Payment>> GetByUserIdAsync(int userId);
    
    /// <summary>
    /// Get payments by hotel ID
    /// </summary>
    /// <param name="hotelId">Hotel ID</param>
    /// <returns>Hotel payments</returns>
    Task<IEnumerable<Payment>> GetByHotelIdAsync(int hotelId);
    
    /// <summary>
    /// Get payments by status
    /// </summary>
    /// <param name="status">Payment status</param>
    /// <returns>Payments with the specified status</returns>
    Task<IEnumerable<Payment>> GetByStatusAsync(EPaymentStatus status);
    
    /// <summary>
    /// Get payment by booking ID
    /// </summary>
    /// <param name="bookingId">Booking ID</param>
    /// <returns>Payment or null if not found</returns>
    Task<Payment?> GetByBookingIdAsync(int bookingId);
}
