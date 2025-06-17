using System.Net.Mime;
using customhost_backend.crm.Domain.Models.Aggregates;
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
    [SwaggerResponse(201, "Service request created successfully", typeof(ServiceRequest))]
    [SwaggerResponse(400, "Service request can't be created.", null)]
    public async Task<ActionResult> CreateServiceRequest([FromBody] CreateServiceRequestResource serviceRequestResource)
    {
        var command = CreateServiceRequestCommandFromResourceAssembler.ToCommandFromResource(serviceRequestResource);
        var result = await serviceRequestCommandService.Handle(command);
        if (result == null)
            return BadRequest("Service request could not be created.");

        return Ok(CreateServiceRequestResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all service requests",
        Description = "Retrieves a list of all service requests.",
        OperationId = "GetServiceRequests")]
    [SwaggerResponse(200, "Service requests retrieved successfully", typeof(IEnumerable<ServiceRequestResource>))]
    public async Task<ActionResult> GetServiceRequests()
    {
        var serviceRequests = (await serviceRequestQueryService.Handle()).ToList();
        if (serviceRequests.Count == 0)
            return NotFound("No service requests found.");

        var resources = CreateServiceRequestResourceFromEntityAssembler.ToResourcesFromEntities(serviceRequests);
        return Ok(resources);
    }
}
