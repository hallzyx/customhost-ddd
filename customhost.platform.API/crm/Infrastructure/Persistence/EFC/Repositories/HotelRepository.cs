using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.crm.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Hotel repository implementation using Entity Framework Core
/// </summary>
public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
{
    public HotelRepository(AppDbContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Hotel>> FindByAdminIdAsync(int adminId)
    {
        return await Context.Set<Hotel>()
            .Where(h => h.AdminId == adminId)
            .ToListAsync();
    }    /// <inheritdoc />
    public async Task<Hotel?> FindByEmailAsync(string email)
    {
        return await Context.Set<Hotel>()
            .FirstOrDefaultAsync(h => h.Email == email.ToLowerInvariant());
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await Context.Set<Hotel>()
            .AnyAsync(h => h.Email == email.ToLowerInvariant());
    }
}
