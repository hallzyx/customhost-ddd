using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.Queries;

namespace customhost_backend.crm.Domain.Services;

/// <summary>
/// Hotel query service interface
/// </summary>
public interface IHotelQueryService
{
    /// <summary>
    /// Handle get all hotels query
    /// </summary>
    /// <param name="query">Get all hotels query</param>
    /// <returns>List of all hotels</returns>
    Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query);
    
    /// <summary>
    /// Handle get hotel by id query
    /// </summary>
    /// <param name="query">Get hotel by id query</param>
    /// <returns>Hotel if found</returns>
    Task<Hotel?> Handle(GetHotelByIdQuery query);
    
    /// <summary>
    /// Handle get hotels by admin id query
    /// </summary>
    /// <param name="query">Get hotels by admin id query</param>
    /// <returns>List of hotels managed by admin</returns>
    Task<IEnumerable<Hotel>> Handle(GetHotelsByAdminIdQuery query);
}
