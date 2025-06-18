using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.CommandServices;

/// <summary>
/// Notification Command Service
/// </summary>
/// <remarks>
/// This class represents the notification command service.
/// It contains the methods to handle notification commands.
/// </remarks>
public class NotificationCommandService(
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork) 
    : INotificationCommandService
{
    /// <inheritdoc />
    public async Task<Notification?> Handle(CreateNotificationCommand command)
    {
        var notification = new Notification(command.UserId, command.Title, command.Message, command.Type);
        try
        {
            await notificationRepository.AddAsync(notification);
            await unitOfWork.CompleteAsync();
            return notification;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<Notification?> Handle(MarkNotificationAsReadCommand command)
    {
        try
        {
            var notification = await notificationRepository.GetByIdAsync(command.NotificationId);
            if (notification == null) return null;

            notification.MarkAsRead();
            await notificationRepository.UpdateAsync(notification);
            await unitOfWork.CompleteAsync();
            return notification;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
