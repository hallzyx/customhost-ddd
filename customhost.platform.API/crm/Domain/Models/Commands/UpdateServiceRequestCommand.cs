using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Commands;

/// <summary>
/// Update Service Request Command
/// </summary>
/// <param name="Id">Service request ID</param>
/// <param name="UserId">User ID</param>
/// <param name="HotelId">Hotel ID</param>
/// <param name="RoomId">Room ID</param>
/// <param name="Type">Service request type</param>
/// <param name="Category">Service request category</param>
/// <param name="Description">Service request description</param>
/// <param name="Status">Service request status</param>
/// <param name="Priority">Service request priority</param>
/// <param name="AssignedTo">Assigned staff member ID (optional)</param>
public record UpdateServiceRequestCommand(
    int Id,
    int UserId,
    int HotelId,
    int RoomId,
    EServiceRequestType Type,
    string Category,
    string Description,
    EServiceRequestStatus Status,
    EServiceRequestPriority Priority,
    int? AssignedTo = null
);
