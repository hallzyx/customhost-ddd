using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform notification entity to notification resource
/// </summary>
public static class NotificationResourceFromEntityAssembler
{
    /// <summary>
    /// Transform notification entity to notification resource
    /// </summary>
    /// <param name="entity">Notification entity</param>
    /// <returns>Notification resource</returns>
    public static NotificationResource ToResourceFromEntity(Notification entity)
    {
        return new NotificationResource(
            entity.Id,
            entity.UserId,
            entity.Title,
            entity.Message,
            entity.Type,
            entity.Read,
            entity.CreatedAt
        );
    }

    /// <summary>
    /// Transform notification entities to notification resources
    /// </summary>
    /// <param name="entities">Notification entities</param>
    /// <returns>Notification resources</returns>
    public static IEnumerable<NotificationResource> ToResourceFromEntity(IEnumerable<Notification> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}
