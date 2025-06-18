using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class StaffMemberResourceFromEntityAssembler
{
    public static StaffMemberResource ToResourceFromEntity(StaffMember entity)
    {
        return new StaffMemberResource(
            entity.Id,
            entity.HotelId,
            entity.FirstName,
            entity.LastName,
            entity.FullName,
            entity.Email,
            entity.Phone,
            entity.Status.ToString(),
            entity.Department.ToString(),
            entity.CreatedAt
        );
    }
}
