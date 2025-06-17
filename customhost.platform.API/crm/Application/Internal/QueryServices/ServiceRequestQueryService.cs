using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.QueryServices;

public class ServiceRequestQueryService
(IServiceRequestRepository serviceRequestRepository, IUnitOfWork unitOfWork)
: IServiceRequestQueryService
{
    public Task<IEnumerable<ServiceRequest>> Handle()
    {
        return serviceRequestRepository.ListAsync();
    }
}
