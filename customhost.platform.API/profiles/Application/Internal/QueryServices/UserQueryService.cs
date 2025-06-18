using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.profiles.Domain.Services;

namespace customhost_backend.profiles.Application.Internal.QueryServices;

/// <summary>
/// User Query Service Implementation
/// </summary>
public class UserQueryService(IUserRepository userRepository) 
    : IUserQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await userRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<User?> GetByIdAsync(int id)
    {
        return await userRepository.FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await userRepository.FindByEmailAsync(email);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetByHotelIdAsync(int hotelId)
    {
        return await userRepository.FindByHotelIdAsync(hotelId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetByRoleAsync(EUserRole role)
    {
        return await userRepository.FindByRoleAsync(role);
    }
}
