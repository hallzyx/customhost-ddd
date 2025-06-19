namespace customhost_backend.crm.Interfaces.REST.Resources;

public record ServiceRequestResource(
    int Id,
    string Title,
    string Description, 
    string Type,
    string Status,
    string Priority,
    int UserId,
    int HotelId,
    int RoomId,
    int? AssignedTo,
    DateTime CreatedAt,
    DateTime? ResolvedAt,
    DateTime? CompletedAt,
    List<string> History
);
