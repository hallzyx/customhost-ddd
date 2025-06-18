namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Create Hotel Command 
/// </summary>
/// <param name="Name">
/// The name of the hotel.
/// </param>
/// <param name="Address">
/// The address of the hotel.
/// </param>
/// <param name="Email">
/// The email address of the hotel.
/// </param>
/// <param name="Phone">
/// The phone number of the hotel.
/// </param>
/// <param name="AdminId">
/// The admin ID responsible for the hotel.
/// </param>
public record CreateHotelCommand(string Name, string Address, string Email, string Phone, int AdminId)
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Hotel name is required", nameof(Name));
        
        if (string.IsNullOrWhiteSpace(Address))
            throw new ArgumentException("Hotel address is required", nameof(Address));
        
        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("Hotel email is required", nameof(Email));
        
        if (string.IsNullOrWhiteSpace(Phone))
            throw new ArgumentException("Hotel phone is required", nameof(Phone));
        
        if (AdminId <= 0)
            throw new ArgumentException("Valid admin ID is required", nameof(AdminId));
        
        if (Name.Length > 200)
            throw new ArgumentException("Hotel name cannot exceed 200 characters", nameof(Name));
        
        if (Address.Length > 500)
            throw new ArgumentException("Hotel address cannot exceed 500 characters", nameof(Address));
        
        if (Phone.Length > 20)
            throw new ArgumentException("Hotel phone cannot exceed 20 characters", nameof(Phone));
    }
};
