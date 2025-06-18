using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Service Request Command Service Interface
/// </summary>
public interface IServiceRequestCommandService
{
    /// <summary>
    /// Handle create service request command
    /// </summary>
    /// <param name="command">Create service request command</param>
    /// <returns>Created service request or null if failed</returns>
    Task<ServiceRequest?> Handle(CreateServiceRequestCommand command);
    
    /// <summary>
    /// Handle update service request command
    /// </summary>
    /// <param name="command">Update service request command</param>
    /// <returns>Updated service request or null if failed</returns>
    Task<ServiceRequest?> Handle(UpdateServiceRequestCommand command);
    
    /// <summary>
    /// Handle assign staff to service request command
    /// </summary>
    /// <param name="command">Assign staff command</param>
    /// <returns>Updated service request or null if failed</returns>
    Task<ServiceRequest?> Handle(AssignStaffToServiceRequestCommand command);
    
    /// <summary>
    /// Handle resolve service request command
    /// </summary>
    /// <param name="command">Resolve service request command</param>
    /// <returns>Updated service request or null if failed</returns>
    Task<ServiceRequest?> Handle(ResolveServiceRequestCommand command);
    
    /// <summary>
    /// Handle delete service request command
    /// </summary>
    /// <param name="command">Delete service request command</param>
    /// <returns>True if deleted, false otherwise</returns>
    Task<bool> Handle(DeleteServiceRequestCommand command);
}