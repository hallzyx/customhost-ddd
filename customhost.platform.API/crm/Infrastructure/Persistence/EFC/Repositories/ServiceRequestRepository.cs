using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace customhost_backend.crm.Infrastructure.Repositories;

public class ServiceRequestRepository(AppDbContext context )
: BaseRepository<ServiceRequest>(context),IServiceRequestRepository
{
    
}