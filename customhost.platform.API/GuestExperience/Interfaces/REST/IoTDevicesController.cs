using System.Net.Mime;
using customhost_backend.GuestExperience.Domain.Model.Commands;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.GuestExperience.Interfaces.REST.Resources;
using customhost_backend.GuestExperience.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.GuestExperience.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available IoT Device Endpoints")]
public class IoTDevicesController(
    IIoTDeviceCommandService iotDeviceCommandService,
    IIoTDeviceQueryService iotDeviceQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all IoT devices",
        Description = "Get all available IoT devices.",
        OperationId = "GetAllIoTDevices")]
    [SwaggerResponse(StatusCodes.Status200OK, "IoT devices found", typeof(IEnumerable<IoTDeviceResource>))]
    public async Task<IActionResult> GetAllIoTDevices()
    {
        var getAllIoTDevicesQuery = new GetAllIoTDevicesQuery();
        var iotDevices = await iotDeviceQueryService.Handle(getAllIoTDevicesQuery);
        var iotDeviceResources = iotDevices.Select(IoTDeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(iotDeviceResources);
    }

    [HttpGet("{iotDeviceId:int}")]
    [SwaggerOperation(
        Summary = "Gets an IoT device by its ID",
        Description = "Get an IoT device by given device ID.",
        OperationId = "GetIoTDeviceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "IoT device found", typeof(IoTDeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "IoT device not found")]
    public async Task<IActionResult> GetIoTDeviceById([FromRoute] int iotDeviceId)
    {
        var getIoTDeviceByIdQuery = new GetIoTDeviceByIdQuery(iotDeviceId);
        var iotDevice = await iotDeviceQueryService.Handle(getIoTDeviceByIdQuery);
        if (iotDevice is null) return NotFound();
        var iotDeviceResource = IoTDeviceResourceFromEntityAssembler.ToResourceFromEntity(iotDevice);
        return Ok(iotDeviceResource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new IoT device",
        Description = "Creates a new IoT device with the provided details.",
        OperationId = "CreateIoTDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "IoT device created successfully", typeof(IoTDeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid IoT device data")]
    public async Task<IActionResult> CreateIoTDevice([FromBody] CreateIoTDeviceResource resource)
    {
        var createIoTDeviceCommand = CreateIoTDeviceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var iotDevice = await iotDeviceCommandService.Handle(createIoTDeviceCommand);
        if (iotDevice is null) return BadRequest("IoT device could not be created.");
        var iotDeviceResource = IoTDeviceResourceFromEntityAssembler.ToResourceFromEntity(iotDevice);
        return CreatedAtAction(nameof(GetIoTDeviceById), new { iotDeviceId = iotDevice.Id }, iotDeviceResource);
    }

    [HttpPut("{iotDeviceId:int}")]
    [SwaggerOperation(
        Summary = "Updates an existing IoT device",
        Description = "Updates an existing IoT device with the provided details.",
        OperationId = "UpdateIoTDevice")]
    [SwaggerResponse(StatusCodes.Status200OK, "IoT device updated successfully", typeof(IoTDeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "IoT device not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid IoT device data")]
    public async Task<IActionResult> UpdateIoTDevice([FromRoute] int iotDeviceId, [FromBody] UpdateIoTDeviceResource resource)
    {
        var updateIoTDeviceCommand = UpdateIoTDeviceCommandFromResourceAssembler.ToCommandFromResource(iotDeviceId, resource);
        var iotDevice = await iotDeviceCommandService.Handle(updateIoTDeviceCommand);
        if (iotDevice is null) return NotFound();
        var iotDeviceResource = IoTDeviceResourceFromEntityAssembler.ToResourceFromEntity(iotDevice);
        return Ok(iotDeviceResource);
    }

    [HttpDelete("{iotDeviceId:int}")]
    [SwaggerOperation(
        Summary = "Deletes an IoT device",
        Description = "Deletes an existing IoT device by its ID.",
        OperationId = "DeleteIoTDevice")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "IoT device deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "IoT device not found")]
    public async Task<IActionResult> DeleteIoTDevice([FromRoute] int iotDeviceId)
    {
        var deleteIoTDeviceCommand = new DeleteIoTDeviceCommand(iotDeviceId);
        var result = await iotDeviceCommandService.Handle(deleteIoTDeviceCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}
