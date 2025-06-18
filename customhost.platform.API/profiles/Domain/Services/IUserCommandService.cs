using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.Commands;

namespace customhost_backend.profiles.Domain.Services;

/// <summary>
/// User Command Service Interface
/// </summary>
public interface IUserCommandService
{
    /// <summary>
    /// Handle create user command
    /// </summary>
    /// <param name="command">Create user command</param>
    /// <returns>Created user or null if failed</returns>
    Task<User?> Handle(CreateUserCommand command);
    
    /// <summary>
    /// Handle update user command
    /// </summary>
    /// <param name="command">Update user command</param>
    /// <returns>Updated user or null if failed</returns>
    Task<User?> Handle(UpdateUserCommand command);
    
    /// <summary>
    /// Handle delete user command
    /// </summary>
    /// <param name="command">Delete user command</param>
    /// <returns>True if deleted, false otherwise</returns>
    Task<bool> Handle(DeleteUserCommand command);
}
