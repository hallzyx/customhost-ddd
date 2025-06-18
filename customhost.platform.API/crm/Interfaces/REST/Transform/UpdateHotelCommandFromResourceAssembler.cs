using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform update hotel resource to update hotel command
/// </summary>
public static class UpdateHotelCommandFromResourceAssembler
{
    /// <summary>
    /// Transform update hotel resource to update hotel command
    /// </summary>
    /// <param name="id">Hotel ID</param>
    /// <param name="resource">Update hotel resource</param>
    /// <returns>Update hotel command</returns>
    public static UpdateHotelCommand ToCommandFromResource(int id, UpdateHotelResource resource)
    {
        return new UpdateHotelCommand(
            id,
            resource.Name,
            resource.Address,
            resource.Email,
            resource.Phone
        );
    }
}
