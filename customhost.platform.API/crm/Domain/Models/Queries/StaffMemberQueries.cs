namespace customhost_backend.crm.Domain.Models.Queries;

public record GetAllStaffMembersQuery();
public record GetStaffMemberByIdQuery(int Id);
public record GetStaffMembersByHotelIdQuery(int HotelId);
public record GetStaffMembersByDepartmentQuery(int HotelId, string Department);
public record GetActiveStaffMembersQuery(int HotelId);
