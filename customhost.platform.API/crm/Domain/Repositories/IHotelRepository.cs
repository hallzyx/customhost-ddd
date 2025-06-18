using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Domain.Repositories;

/// <summary>
/// Hotel repository interface
/// </summary>
public interface IHotelRepository : IBaseRepository<Hotel>
{
    /// <summary>
    /// Find hotels by admin ID
    /// </summary>
    /// <param name="adminId">The admin ID</param>
    /// <returns>List of hotels managed by the admin</returns>
    Task<IEnumerable<Hotel>> FindByAdminIdAsync(int adminId);
    
    /// <summary>
    /// Find hotel by email
    /// </summary>
    /// <param name="email">The hotel email</param>
    /// <returns>The hotel if found</returns>
    Task<Hotel?> FindByEmailAsync(string email);
    
    /// <summary>
    /// Check if hotel exists by email
    /// </summary>
    /// <param name="email">The hotel email</param>
    /// <returns>True if exists</returns>
    Task<bool> ExistsByEmailAsync(string email);
}
