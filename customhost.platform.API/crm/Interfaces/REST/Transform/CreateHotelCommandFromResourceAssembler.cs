using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform create hotel resource to create hotel command
/// </summary>
public static class CreateHotelCommandFromResourceAssembler
{
    /// <summary>
    /// Transform create hotel resource to create hotel command
    /// </summary>
    /// <param name="resource">Create hotel resource</param>
    /// <returns>Create hotel command</returns>
    public static CreateHotelCommand ToCommandFromResource(CreateHotelResource resource)
    {
        return new CreateHotelCommand(
            resource.Name,
            resource.Address,
            resource.Email,
            resource.Phone,
            resource.AdminId
        );
    }
}
