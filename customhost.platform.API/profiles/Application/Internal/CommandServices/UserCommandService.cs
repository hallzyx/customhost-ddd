using customhost_backend.profiles.Domain.Models.Aggregates;
using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.profiles.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.profiles.Application.Internal.CommandServices;

/// <summary>
/// User Command Service Implementation
/// </summary>
public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork) 
    : IUserCommandService
{
    /// <inheritdoc />
    public async Task<User?> Handle(CreateUserCommand command)
    {
        try
        {
            // Check if email already exists
            var existingUser = await userRepository.FindByEmailAsync(command.Email);
            if (existingUser != null) return null;

            var user = new User(command);
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            return user;
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<User?> Handle(UpdateUserCommand command)
    {
        try
        {
            var user = await userRepository.FindByIdAsync(command.Id);
            if (user == null) return null;

            // Check if email is being changed and if it already exists
            if (!string.IsNullOrWhiteSpace(command.Email) && command.Email != user.Email)
            {
                var existingUser = await userRepository.FindByEmailAsync(command.Email);
                if (existingUser != null) return null;
            }

            user.UpdateUser(command);
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
            return user;
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteUserCommand command)
    {
        try
        {
            var user = await userRepository.FindByIdAsync(command.Id);
            if (user == null) return false;

            userRepository.Remove(user);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
