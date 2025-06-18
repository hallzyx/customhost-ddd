using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Application.Internal.CommandServices;

/// <summary>
/// Service Request Command Service Implementation
/// </summary>
public class ServiceRequestCommandService(
    IServiceRequestRepository serviceRequestRepository, 
    IUnitOfWork unitOfWork)
    : IServiceRequestCommandService
{
    /// <inheritdoc />
    public async Task<ServiceRequest?> Handle(CreateServiceRequestCommand command)
    {
        try
        {
            var serviceRequest = new ServiceRequest(command);
            await serviceRequestRepository.AddAsync(serviceRequest);
            await unitOfWork.CompleteAsync();
            return serviceRequest;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ServiceRequest?> Handle(UpdateServiceRequestCommand command)
    {
        try
        {
            var serviceRequest = await serviceRequestRepository.FindByIdAsync(command.Id);
            if (serviceRequest == null) return null;

            serviceRequest.UpdateServiceRequest(command);
            serviceRequestRepository.Update(serviceRequest);
            await unitOfWork.CompleteAsync();
            return serviceRequest;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ServiceRequest?> Handle(AssignStaffToServiceRequestCommand command)
    {
        try
        {
            var serviceRequest = await serviceRequestRepository.FindByIdAsync(command.ServiceRequestId);
            if (serviceRequest == null) return null;

            serviceRequest.AssignStaff(command.StaffId);
            serviceRequestRepository.Update(serviceRequest);
            await unitOfWork.CompleteAsync();
            return serviceRequest;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<ServiceRequest?> Handle(ResolveServiceRequestCommand command)
    {
        try
        {
            var serviceRequest = await serviceRequestRepository.FindByIdAsync(command.ServiceRequestId);
            if (serviceRequest == null) return null;

            serviceRequest.Resolve();
            serviceRequestRepository.Update(serviceRequest);
            await unitOfWork.CompleteAsync();
            return serviceRequest;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<bool> Handle(DeleteServiceRequestCommand command)
    {
        try
        {
            var serviceRequest = await serviceRequestRepository.FindByIdAsync(command.Id);
            if (serviceRequest == null) return false;

            serviceRequestRepository.Remove(serviceRequest);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}