using System.Net.Mime;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Interfaces.REST.Resources;
using customhost_backend.crm.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.crm.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Notification Endpoints.")]
public class NotificationsController(
    INotificationCommandService notificationCommandService,
    INotificationQueryService notificationQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Notifications", "Get all notifications with optional filtering by user.", OperationId = "GetAllNotifications")]
    [SwaggerResponse(200, "The notifications were found and returned.", typeof(IEnumerable<NotificationResource>))]
    public async Task<IActionResult> GetAllNotifications([FromQuery] int? userId = null)
    {
        IEnumerable<customhost_backend.crm.Domain.Models.Aggregates.Notification> notifications;
        
        if (userId.HasValue)
        {
            notifications = await notificationQueryService.GetByUserIdAsync(userId.Value);
        }
        else
        {
            notifications = await notificationQueryService.GetAllAsync();
        }

        var notificationResources = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notifications);
        return Ok(notificationResources);
    }

    [HttpGet("{notificationId:int}")]
    [SwaggerOperation("Get Notification by Id", "Get a notification by its unique identifier.", OperationId = "GetNotificationById")]
    [SwaggerResponse(200, "The notification was found and returned.", typeof(NotificationResource))]
    [SwaggerResponse(404, "The notification was not found.")]
    public async Task<IActionResult> GetNotificationById(int notificationId)
    {
        var notification = await notificationQueryService.GetByIdAsync(notificationId);
        if (notification is null) return NotFound();
        
        var notificationResource = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification);
        return Ok(notificationResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Notification", "Create a new notification.", OperationId = "CreateNotification")]
    [SwaggerResponse(201, "The notification was created.", typeof(NotificationResource))]
    [SwaggerResponse(400, "The notification was not created.")]
    public async Task<IActionResult> CreateNotification(CreateNotificationResource resource)
    {
        var createNotificationCommand = CreateNotificationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var notification = await notificationCommandService.Handle(createNotificationCommand);
        if (notification is null) return BadRequest();
        
        var notificationResource = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification);
        return CreatedAtAction(nameof(GetNotificationById), new { notificationId = notification.Id }, notificationResource);
    }

    [HttpPatch("{notificationId:int}")]
    [SwaggerOperation("Mark Notification As Read", "Mark a notification as read.", OperationId = "MarkNotificationAsRead")]
    [SwaggerResponse(200, "The notification was marked as read.", typeof(NotificationResource))]
    [SwaggerResponse(404, "The notification was not found.")]
    [SwaggerResponse(400, "The notification could not be updated.")]
    public async Task<IActionResult> MarkNotificationAsRead(int notificationId, MarkNotificationAsReadResource resource)
    {
        var markAsReadCommand = new MarkNotificationAsReadCommand(notificationId);
        var notification = await notificationCommandService.Handle(markAsReadCommand);
        if (notification is null) return NotFound();
        
        var notificationResource = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notification);
        return Ok(notificationResource);
    }

    [HttpGet("users/{userId:int}/unread")]
    [SwaggerOperation("Get Unread Notifications by User", "Get all unread notifications for a specific user.", OperationId = "GetUnreadNotificationsByUser")]
    [SwaggerResponse(200, "The unread notifications were found and returned.", typeof(IEnumerable<NotificationResource>))]
    public async Task<IActionResult> GetUnreadNotificationsByUser(int userId)
    {
        var notifications = await notificationQueryService.GetUnreadByUserIdAsync(userId);
        var notificationResources = NotificationResourceFromEntityAssembler.ToResourceFromEntity(notifications);
        return Ok(notificationResources);
    }
}
