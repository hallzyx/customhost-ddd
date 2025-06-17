using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Models.Aggregates;

public class StaffMember
{
    public int Id { get; }
    public int HotelId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Department { get; set; }
    public DateTime CreatedAt { get; set; }

    public StaffMember() { }

    public StaffMember(CreateStaffMemberCommand command)
    {
        this.HotelId = command.HotelId;
        this.FirstName = command.FirstName;
        this.LastName = command.LastName;
        this.Email = command.Email;
        this.PhoneNumber = command.PhoneNumber;
        this.Department = command.Department;
        this.CreatedAt = new DateTime();
    }
}
    
