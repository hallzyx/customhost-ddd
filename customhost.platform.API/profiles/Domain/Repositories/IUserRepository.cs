using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.profiles.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
    Task<IEnumerable<User>> FindByHotelIdAsync(int hotelId);
    Task<IEnumerable<User>> FindByRoleAsync(EUserRole role);
}
