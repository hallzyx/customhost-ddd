using customhost_backend.crm.Domain.Models.Aggregates;

namespace customhost_backend.crm.Domain.Repositories;

/// <summary>
/// Notification Repository Interface
/// </summary>
/// <remarks>
/// This interface defines the contract for notification repository operations.
/// </remarks>
public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<Notification?> GetByIdAsync(int id);
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    Task<Notification> AddAsync(Notification notification);
    Task<Notification> UpdateAsync(Notification notification);
    Task DeleteAsync(int id);
}
