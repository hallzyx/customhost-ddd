using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Interfaces.REST.Resources;

namespace customhost_backend.crm.Interfaces.REST.Transform;

public static class CreateServiceRequestResourceFromEntityAssembler
{
    public static ServiceRequestResource ToResourceFromEntity(ServiceRequest serviceRequest)
    {
        return new ServiceRequestResource(
            serviceRequest.Id,
            serviceRequest.UserId,
            serviceRequest.HotelId,
            serviceRequest.RoomId,
            serviceRequest.Type,
            serviceRequest.Description,
            serviceRequest.Status,
            serviceRequest.AsignedTo,
            serviceRequest.CreatedAt,
            serviceRequest.CompleteAt
        );
    }

    public static List<ServiceRequestResource> ToResourcesFromEntities(IEnumerable<ServiceRequest> serviceRequests)
    {
        return serviceRequests.Select(serviceRequest => ToResourceFromEntity(serviceRequest)).ToList();
    }
}
