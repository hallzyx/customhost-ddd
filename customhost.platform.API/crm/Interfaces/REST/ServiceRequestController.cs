using System.Net.Mime;
using customhost_backend.crm.Domain.Models.Aggregates;
using customhost_backend.crm.Domain.Models.ValueObjects;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Interfaces.REST.Resources;
using customhost_backend.crm.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.crm.Interfaces.REST;

[ApiController]
[Route("api/v1/crm/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("ServiceRequests")]
public class ServiceRequestController(
    IServiceRequestCommandService serviceRequestCommandService,
    IServiceRequestQueryService serviceRequestQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new service request",
        Description = "Creates a new service request with the specified details.",
        OperationId = "CreateServiceRequest")]
    [SwaggerResponse(201, "Service request created successfully", typeof(ServiceRequestResource))]
    [SwaggerResponse(400, "Service request can't be created.", null)]
    public async Task<ActionResult> CreateServiceRequest([FromBody] CreateServiceRequestResource serviceRequestResource)
    {
        var command = CreateServiceRequestCommandFromResourceAssembler.ToCommandFromResource(serviceRequestResource);
        var result = await serviceRequestCommandService.Handle(command);
        if (result == null)
            return BadRequest("Service request could not be created.");

        return CreatedAtAction(nameof(GetServiceRequestById), new { id = result.Id }, 
            ServiceRequestResourceFromEntityAssembler.ToResourceFromEntity(result));
    }    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all service requests",
        Description = "Retrieves a list of all service requests.",
        OperationId = "GetServiceRequests")]
    [SwaggerResponse(200, "Service requests retrieved successfully", typeof(IEnumerable<ServiceRequestResource>))]
    public async Task<ActionResult> GetServiceRequests()
    {
        var serviceRequests = (await serviceRequestQueryService.GetAllAsync()).ToList();
        if (serviceRequests.Count == 0)
            return Ok(new List<ServiceRequestResource>());

        var resources = ServiceRequestResourceFromEntityAssembler.ToResourcesFromEntities(serviceRequests);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get service request by ID",
        Description = "Retrieves a specific service request by its ID.",
        OperationId = "GetServiceRequestById")]
    [SwaggerResponse(200, "Service request retrieved successfully", typeof(ServiceRequestResource))]
    [SwaggerResponse(404, "Service request not found", null)]
    public async Task<ActionResult> GetServiceRequestById(int id)
    {
        var serviceRequest = await serviceRequestQueryService.GetByIdAsync(id);
        if (serviceRequest == null)
            return NotFound($"Service request with ID {id} not found.");

        var resource = ServiceRequestResourceFromEntityAssembler.ToResourceFromEntity(serviceRequest);
        return Ok(resource);
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation(
        Summary = "Get service requests by user ID",
        Description = "Retrieves all service requests for a specific user.",
        OperationId = "GetServiceRequestsByUserId")]
    [SwaggerResponse(200, "Service requests retrieved successfully", typeof(IEnumerable<ServiceRequestResource>))]
    public async Task<ActionResult> GetServiceRequestsByUserId(int userId)
    {
        var serviceRequests = (await serviceRequestQueryService.GetByUserIdAsync(userId)).ToList();
        var resources = ServiceRequestResourceFromEntityAssembler.ToResourcesFromEntities(serviceRequests);
        return Ok(resources);
    }

    [HttpGet("hotel/{hotelId:int}")]
    [SwaggerOperation(
        Summary = "Get service requests by hotel ID",
        Description = "Retrieves all service requests for a specific hotel.",
        OperationId = "GetServiceRequestsByHotelId")]
    [SwaggerResponse(200, "Service requests retrieved successfully", typeof(IEnumerable<ServiceRequestResource>))]
    public async Task<ActionResult> GetServiceRequestsByHotelId(int hotelId)
    {
        var serviceRequests = (await serviceRequestQueryService.GetByHotelIdAsync(hotelId)).ToList();
        var resources = ServiceRequestResourceFromEntityAssembler.ToResourcesFromEntities(serviceRequests);
        return Ok(resources);
    }

    [HttpGet("room/{roomId:int}")]
    [SwaggerOperation(
        Summary = "Get service requests by room ID",
        Description = "Retrieves all service requests for a specific room.",
        OperationId = "GetServiceRequestsByRoomId")]
    [SwaggerResponse(200, "Service requests retrieved successfully", typeof(IEnumerable<ServiceRequestResource>))]
    public async Task<ActionResult> GetServiceRequestsByRoomId(int roomId)
    {
        var serviceRequests = (await serviceRequestQueryService.GetByRoomIdAsync(roomId)).ToList();
        var resources = ServiceRequestResourceFromEntityAssembler.ToResourcesFromEntities(serviceRequests);
        return Ok(resources);
    }

    [HttpGet("status/{status}")]
    [SwaggerOperation(
        Summary = "Get service requests by status",
        Description = "Retrieves all service requests with a specific status.",
        OperationId = "GetServiceRequestsByStatus")]
    [SwaggerResponse(200, "Service requests retrieved successfully", typeof(IEnumerable<ServiceRequestResource>))]
    [SwaggerResponse(400, "Invalid status value", null)]
    public async Task<ActionResult> GetServiceRequestsByStatus(string status)
    {
        var serviceRequests = (await serviceRequestQueryService.GetByStatusAsync(status)).ToList();
        var resources = ServiceRequestResourceFromEntityAssembler.ToResourcesFromEntities(serviceRequests);
        return Ok(resources);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a service request",
        Description = "Updates an existing service request with new information.",
        OperationId = "UpdateServiceRequest")]
    [SwaggerResponse(200, "Service request updated successfully", typeof(ServiceRequestResource))]
    [SwaggerResponse(404, "Service request not found", null)]
    [SwaggerResponse(400, "Service request update failed", null)]
    public async Task<ActionResult> UpdateServiceRequest(int id, [FromBody] UpdateServiceRequestResource updateResource)
    {
        var command = UpdateServiceRequestCommandFromResourceAssembler.ToCommandFromResource(id, updateResource);
        var result = await serviceRequestCommandService.Handle(command);
        if (result == null)
            return NotFound($"Service request with ID {id} not found.");

        var resource = ServiceRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpPatch("{id:int}/assign")]
    [SwaggerOperation(
        Summary = "Assign staff to service request",
        Description = "Assigns a staff member to handle a service request.",
        OperationId = "AssignStaffToServiceRequest")]
    [SwaggerResponse(200, "Staff assigned successfully", typeof(ServiceRequestResource))]
    [SwaggerResponse(404, "Service request not found", null)]
    [SwaggerResponse(400, "Staff assignment failed", null)]
    public async Task<ActionResult> AssignStaffToServiceRequest(int id, [FromBody] AssignStaffToServiceRequestResource assignResource)
    {
        var command = AssignStaffToServiceRequestCommandFromResourceAssembler.ToCommandFromResource(id, assignResource);
        var result = await serviceRequestCommandService.Handle(command);
        if (result == null)
            return NotFound($"Service request with ID {id} not found.");

        var resource = ServiceRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpPatch("{id:int}/resolve")]
    [SwaggerOperation(
        Summary = "Resolve service request",
        Description = "Marks a service request as resolved.",
        OperationId = "ResolveServiceRequest")]
    [SwaggerResponse(200, "Service request resolved successfully", typeof(ServiceRequestResource))]
    [SwaggerResponse(404, "Service request not found", null)]
    [SwaggerResponse(400, "Service request resolution failed", null)]
    public async Task<ActionResult> ResolveServiceRequest(int id)
    {
        var command = new customhost_backend.crm.Domain.Models.Commands.ResolveServiceRequestCommand(id);
        var result = await serviceRequestCommandService.Handle(command);
        if (result == null)
            return NotFound($"Service request with ID {id} not found.");

        var resource = ServiceRequestResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete service request",
        Description = "Deletes a service request from the system.",
        OperationId = "DeleteServiceRequest")]
    [SwaggerResponse(200, "Service request deleted successfully")]
    [SwaggerResponse(404, "Service request not found", null)]
    [SwaggerResponse(400, "Service request deletion failed", null)]
    public async Task<ActionResult> DeleteServiceRequest(int id)
    {
        var command = new customhost_backend.crm.Domain.Models.Commands.DeleteServiceRequestCommand(id);
        var result = await serviceRequestCommandService.Handle(command);
        if (!result)
            return NotFound($"Service request with ID {id} not found.");

        return Ok($"Service request with ID {id} deleted successfully.");
    }
}
