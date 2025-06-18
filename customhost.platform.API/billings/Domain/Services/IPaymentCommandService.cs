using customhost_backend.billings.Domain.Models.Aggregates;
using customhost_backend.billings.Domain.Models.Commands;

namespace customhost_backend.billings.Domain.Services;

/// <summary>
/// Payment Command Service Interface
/// </summary>
public interface IPaymentCommandService
{
    /// <summary>
    /// Handle create payment command
    /// </summary>
    /// <param name="command">Create payment command</param>
    /// <returns>Created payment or null if failed</returns>
    Task<Payment?> Handle(CreatePaymentCommand command);
    
    /// <summary>
    /// Handle update payment command
    /// </summary>
    /// <param name="command">Update payment command</param>
    /// <returns>Updated payment or null if failed</returns>
    Task<Payment?> Handle(UpdatePaymentCommand command);
    
    /// <summary>
    /// Handle delete payment command
    /// </summary>
    /// <param name="command">Delete payment command</param>
    /// <returns>True if deleted, false otherwise</returns>
    Task<bool> Handle(DeletePaymentCommand command);
}
