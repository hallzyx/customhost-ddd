using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Aggregates;

/// <summary>
/// Hotel Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Hotel aggregate root.
/// It contains the properties and methods to manage hotel information.
/// </remarks>
public class Hotel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public HotelStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int AdminId { get; private set; }
    
    public string EmailAddress => Email;
    public bool IsActive => Status == HotelStatus.Active;
    
    // For EF Core
    protected Hotel() 
    {
        Name = string.Empty;
        Address = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Status = HotelStatus.Inactive;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Hotel(string name, string address, string email, string phone, int adminId)
    {
        ValidateEmail(email);
        Name = name;
        Address = address;
        Email = email.ToLowerInvariant();
        Phone = phone;
        Status = HotelStatus.Active;
        CreatedAt = DateTime.UtcNow;
        AdminId = adminId;
    }

    public Hotel(CreateHotelCommand command)
    {
        ValidateEmail(command.Email);
        Name = command.Name;
        Address = command.Address;
        Email = command.Email.ToLowerInvariant();
        Phone = command.Phone;
        Status = HotelStatus.Active;
        CreatedAt = DateTime.UtcNow;
        AdminId = command.AdminId;
    }
    
    public void UpdateInfo(string name, string address, string email, string phone)
    {
        ValidateEmail(email);
        Name = name;
        Address = address;
        Email = email.ToLowerInvariant();
        Phone = phone;
    }
    
    public void Activate()
    {
        Status = HotelStatus.Active;
    }
    
    public void Deactivate()
    {
        Status = HotelStatus.Inactive;
    }
    
    public void Suspend()
    {
        Status = HotelStatus.Suspended;
    }
    
    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email address cannot be null or empty", nameof(email));

        var emailValidation = new EmailAddress(email); // This will validate the format
    }
}
