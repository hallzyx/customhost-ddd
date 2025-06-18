namespace customhost_backend.crm.Interfaces.REST.Resources;

public record CreateStaffMemberResource(
    int HotelId,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Department
);
