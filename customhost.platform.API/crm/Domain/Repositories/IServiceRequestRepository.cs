using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.Shared.Domain.Repositories;

namespace customhost_backend.crm.Domain.Repositories;

public interface IServiceRequestRepository : IBaseRepository<ServiceRequest>
{
    Task<IEnumerable<ServiceRequest>> FindByUserIdAsync(int userId);
    Task<IEnumerable<ServiceRequest>> FindByHotelIdAsync(int hotelId);
    Task<IEnumerable<ServiceRequest>> FindByRoomIdAsync(int roomId);
    Task<IEnumerable<ServiceRequest>> FindByStatusAsync(EServiceRequestStatus status);
}