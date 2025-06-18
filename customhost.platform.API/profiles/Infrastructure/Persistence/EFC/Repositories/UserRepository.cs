using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace customhost_backend.profiles.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) 
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> FindByHotelIdAsync(int hotelId)
    {
        return await Context.Set<User>()
            .Where(u => u.HotelId == hotelId)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> FindByRoleAsync(EUserRole role)
    {
        return await Context.Set<User>()
            .Where(u => u.Role == role)
            .ToListAsync();
    }
}
