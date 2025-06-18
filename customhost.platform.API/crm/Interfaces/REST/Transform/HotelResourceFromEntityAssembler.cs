using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform hotel entity to hotel resource
/// </summary>
public static class HotelResourceFromEntityAssembler
{
    /// <summary>
    /// Transform hotel entity to hotel resource
    /// </summary>
    /// <param name="entity">Hotel entity</param>
    /// <returns>Hotel resource</returns>
    public static HotelResource ToResourceFromEntity(Hotel entity)
    {
        return new HotelResource(
            entity.Id,
            entity.Name,
            entity.Address,
            entity.EmailAddress,
            entity.Phone,
            entity.Status.ToString().ToLowerInvariant(),
            entity.CreatedAt,
            entity.AdminId
        );
    }
}
