namespace customhost_backend.crm.Interfaces.REST.Resources;

public record StaffMemberResource(
    int Id,
    int HotelId,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string Phone,
    string Status,
    string Department,
    DateTime CreatedAt
);
