namespace customhost_backend.crm.Interfaces.REST.Resources;

public record UpdateStaffMemberResource(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Department
);
