using System.Net.Mime;
using customhost_backend.analytics.Domain.Models.Queries;
using customhost_backend.analytics.Domain.Services;
using customhost_backend.analytics.Interfaces.REST.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.analytics.Interfaces.REST;

[ApiController]
[Route("api/v1/analytics")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Analytics Endpoints")]
public class AnalyticsController(
    IAnalyticsSnapshotQueryService analyticsSnapshotQueryService,
    IAnalyticsMetricQueryService analyticsMetricQueryService
) : ControllerBase
{
    [HttpGet("iot-devices/online-status")]
    [SwaggerOperation(
        Summary = "Gets IoT devices online status",
        Description = "Returns the current status of IoT devices including online/offline counts and percentages.",
        OperationId = "GetIoTDevicesOnlineStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "IoT devices status retrieved successfully", typeof(IoTDevicesOnlineStatusResource))]
    public async Task<IActionResult> GetIoTDevicesOnlineStatus()
    {
        var query = new GetIoTDevicesOnlineStatusQuery();
        var result = await analyticsSnapshotQueryService.Handle(query);
        return Ok(result);
    }

    [HttpGet("rooms/occupancy-status")]
    [SwaggerOperation(
        Summary = "Gets rooms occupancy status",
        Description = "Returns the current occupancy status of hotel rooms including availability and occupancy rates.",
        OperationId = "GetRoomsOccupancyStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "Rooms occupancy status retrieved successfully", typeof(RoomsOccupancyStatusResource))]
    public async Task<IActionResult> GetRoomsOccupancyStatus()
    {
        var query = new GetRoomsOccupancyStatusQuery();
        var result = await analyticsSnapshotQueryService.Handle(query);
        return Ok(result);
    }

    [HttpGet("revenue/monthly-trend")]
    [SwaggerOperation(
        Summary = "Gets monthly revenue trend",
        Description = "Returns monthly revenue trends for the specified number of months for chart visualization.",
        OperationId = "GetMonthlyRevenueTrend")]
    [SwaggerResponse(StatusCodes.Status200OK, "Monthly revenue trend retrieved successfully", typeof(MonthlyRevenueTrendResource))]
    public async Task<IActionResult> GetMonthlyRevenueTrend([FromQuery] int months = 6)
    {
        var query = new GetMonthlyRevenueTrendQuery(months);
        var result = await analyticsMetricQueryService.Handle(query);
        return Ok(result);
    }

    [HttpGet("service-requests/monthly-breakdown")]
    [SwaggerOperation(
        Summary = "Gets monthly service requests breakdown by type",
        Description = "Returns monthly breakdown of service requests by type for chart visualization.",
        OperationId = "GetMonthlyServiceRequestsBreakdown")]
    [SwaggerResponse(StatusCodes.Status200OK, "Monthly service requests breakdown retrieved successfully", typeof(MonthlyServiceRequestsBreakdownResource))]
    public async Task<IActionResult> GetMonthlyServiceRequestsBreakdown([FromQuery] int months = 6)
    {
        var query = new GetMonthlyServiceRequestsBreakdownQuery(months);
        var result = await analyticsMetricQueryService.Handle(query);
        return Ok(result);
    }
}
