using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;

namespace customhost_backend.crm.Application.Internal.QueryServices;

/// <summary>
/// Hotel query service implementation
/// </summary>
public class HotelQueryService(IHotelRepository hotelRepository) : IHotelQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query)
    {
        return await hotelRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Hotel?> Handle(GetHotelByIdQuery query)
    {
        return await hotelRepository.FindByIdAsync(query.HotelId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Hotel>> Handle(GetHotelsByAdminIdQuery query)
    {
        return await hotelRepository.FindByAdminIdAsync(query.AdminId);
    }
}
