using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;

namespace customhost_backend.crm.Application.Internal.QueryServices;

/// <summary>
/// Service Request Query Service Implementation
/// </summary>
public class ServiceRequestQueryService(IServiceRequestRepository serviceRequestRepository) 
    : IServiceRequestQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
    {
        return await serviceRequestRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<ServiceRequest?> GetByIdAsync(int id)
    {
        return await serviceRequestRepository.FindByIdAsync(id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ServiceRequest>> GetByUserIdAsync(int userId)
    {
        return await serviceRequestRepository.FindByUserIdAsync(userId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ServiceRequest>> GetByHotelIdAsync(int hotelId)
    {
        return await serviceRequestRepository.FindByHotelIdAsync(hotelId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ServiceRequest>> GetByRoomIdAsync(int roomId)
    {
        return await serviceRequestRepository.FindByRoomIdAsync(roomId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ServiceRequest>> GetByStatusAsync(string status)
    {
        if (Enum.TryParse<EServiceRequestStatus>(status, out var statusEnum))
        {
            return await serviceRequestRepository.FindByStatusAsync(statusEnum);
        }
        return new List<ServiceRequest>();
    }
}
