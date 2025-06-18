using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Models.ValueObjects;

namespace customhost_backend.profiles.Domain.Models.Aggregates;

/// <summary>
/// User Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the User aggregate root.
/// It contains the properties and methods to manage user information.
/// </remarks>
public class User
{
    public int Id { get; private set; }
    public int HotelId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public EUserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // For EF Core
    protected User() { }

    public User(CreateUserCommand command)
    {
        HotelId = command.HotelId;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Email = command.Email;
        PasswordHash = HashPassword(command.Password); // In production, use proper hashing
        Phone = command.Phone;
        Role = command.Role;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateUser(UpdateUserCommand command)
    {
        if (command.HotelId.HasValue)
            HotelId = command.HotelId.Value;

        if (!string.IsNullOrWhiteSpace(command.FirstName))
            FirstName = command.FirstName;

        if (!string.IsNullOrWhiteSpace(command.LastName))
            LastName = command.LastName;

        if (!string.IsNullOrWhiteSpace(command.Email))
            Email = command.Email;

        if (!string.IsNullOrWhiteSpace(command.Phone))
            Phone = command.Phone;

        if (command.Role.HasValue)
            Role = command.Role.Value;
    }

    public void UpdatePassword(string newPassword)
    {
        PasswordHash = HashPassword(newPassword);
    }

    // Business logic properties
    public string FullName => $"{FirstName} {LastName}";
    public bool IsGuest => Role == EUserRole.Guest;
    public bool IsStaff => Role == EUserRole.Staff;
    public bool IsAdmin => Role == EUserRole.Admin;

    // Simple password hashing - in production use BCrypt or similar
    private string HashPassword(string password)
    {
        // This is just for demo purposes - use proper password hashing in production
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password + "salt"));
    }

    public bool VerifyPassword(string password)
    {
        return PasswordHash == HashPassword(password);
    }
}
