using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Aggregates;

/// <summary>
/// Staff Member Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Staff Member aggregate root.
/// It contains the properties and methods to manage staff member information.
/// </remarks>
public class StaffMember
{
    public int Id { get; private set; }
    public int HotelId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public StaffStatus Status { get; private set; }
    public Department Department { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public string FullName => $"{FirstName} {LastName}";
    public bool IsActive => Status == StaffStatus.Active;

    // For EF Core
    protected StaffMember() 
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Status = StaffStatus.Inactive;
        Department = Department.Other;
        CreatedAt = DateTime.UtcNow;
    }

    public StaffMember(int hotelId, string firstName, string lastName, string email, string phone, Department department)
    {
        ValidateStaffMemberData(firstName, lastName, email, phone);
        
        HotelId = hotelId;
        FirstName = firstName;
        LastName = lastName;
        Email = email.ToLowerInvariant();
        Phone = phone;
        Status = StaffStatus.Active;
        Department = department;
        CreatedAt = DateTime.UtcNow;
    }

    public StaffMember(CreateStaffMemberCommand command)
    {
        ValidateStaffMemberData(command.FirstName, command.LastName, command.Email, command.Phone);
        
        HotelId = command.HotelId;
        FirstName = command.FirstName;
        LastName = command.LastName;
        Email = command.Email.ToLowerInvariant();
        Phone = command.Phone;
        Status = StaffStatus.Active;
        Department = command.Department;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void UpdateInfo(string firstName, string lastName, string email, string phone, Department department)
    {
        ValidateStaffMemberData(firstName, lastName, email, phone);
        
        FirstName = firstName;
        LastName = lastName;
        Email = email.ToLowerInvariant();
        Phone = phone;
        Department = department;
    }
    
    public void Activate()
    {
        Status = StaffStatus.Active;
    }
    
    public void Deactivate()
    {
        Status = StaffStatus.Inactive;
    }
    
    public void SetOnLeave()
    {
        Status = StaffStatus.OnLeave;
    }
    
    public void Terminate()
    {
        Status = StaffStatus.Terminated;
    }
    
    private static void ValidateStaffMemberData(string firstName, string lastName, string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required", nameof(firstName));
            
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required", nameof(lastName));
            
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));
            
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone is required", nameof(phone));
            
        // Validate email using the existing EmailAddress value object
        var emailValidation = new EmailAddress(email);
    }
}
    
