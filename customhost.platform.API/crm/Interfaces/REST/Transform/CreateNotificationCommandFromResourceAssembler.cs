using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform create notification resource to create notification command
/// </summary>
public static class CreateNotificationCommandFromResourceAssembler
{
    /// <summary>
    /// Transform create notification resource to create notification command
    /// </summary>
    /// <param name="resource">Create notification resource</param>
    /// <returns>Create notification command</returns>
    public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
    {
        return new CreateNotificationCommand(
            resource.UserId,
            resource.Title,
            resource.Message,
            resource.Type
        );
    }
}
