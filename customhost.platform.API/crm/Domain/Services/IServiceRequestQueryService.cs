using customhost_backend.crm.Domain.Models.Aggregates;

namespace customhost_backend.crm.Domain.Services;

public interface IServiceRequestQueryService
{
    Task<IEnumerable<ServiceRequest>> Handle();
}
