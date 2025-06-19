using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;
using System.Text.Json;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class ServiceRequestResourceFromEntityAssembler
{
    public static ServiceRequestResource ToResourceFromEntity(ServiceRequest serviceRequest)
    {
        var historyList = new List<string>();
        try
        {
            var historyObjects = JsonSerializer.Deserialize<List<object>>(serviceRequest.History);
            historyList = historyObjects?.Select(h => h.ToString() ?? "").ToList() ?? new List<string>();
        }
        catch
        {
            historyList = new List<string>();
        }        return new ServiceRequestResource(
            serviceRequest.Id,
            serviceRequest.Category, // Using Category as Title since Title doesn't exist
            serviceRequest.Description,
            serviceRequest.Type.ToString(),
            serviceRequest.Status.ToString(),
            serviceRequest.Priority.ToString(),
            serviceRequest.UserId,
            serviceRequest.HotelId,
            serviceRequest.RoomId,
            serviceRequest.AssignedTo, // Now int? directly, no conversion needed
            serviceRequest.CreatedAt,
            null, // ResolvedAt doesn't exist, using null
            serviceRequest.CompletedAt,
            historyList
        );
    }

    public static List<ServiceRequestResource> ToResourcesFromEntities(IEnumerable<ServiceRequest> serviceRequests)
    {
        return serviceRequests.Select(serviceRequest => ToResourceFromEntity(serviceRequest)).ToList();
    }
}
