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
[SwaggerTag("Available Room Device Endpoints")]
public class RoomDevicesController(
    IRoomDeviceCommandService roomDeviceCommandService,
    IRoomDeviceQueryService roomDeviceQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all room devices",
        Description = "Get all room devices with their IoT device details.",
        OperationId = "GetAllRoomDevices")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room devices found", typeof(IEnumerable<RoomDeviceResource>))]
    public async Task<IActionResult> GetAllRoomDevices()
    {
        var getAllRoomDevicesQuery = new GetAllRoomDevicesQuery();
        var roomDevices = await roomDeviceQueryService.Handle(getAllRoomDevicesQuery);
        var roomDeviceResources = roomDevices.Select(RoomDeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomDeviceResources);
    }

    [HttpGet("{roomDeviceId:int}")]
    [SwaggerOperation(
        Summary = "Gets a room device by its ID",
        Description = "Get a room device by given device ID.",
        OperationId = "GetRoomDeviceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device found", typeof(RoomDeviceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device not found")]
    public async Task<IActionResult> GetRoomDeviceById([FromRoute] int roomDeviceId)
    {
        var getRoomDeviceByIdQuery = new GetRoomDeviceByIdQuery(roomDeviceId);
        var roomDevice = await roomDeviceQueryService.Handle(getRoomDeviceByIdQuery);
        if (roomDevice is null) return NotFound();
        var roomDeviceResource = RoomDeviceResourceFromEntityAssembler.ToResourceFromEntity(roomDevice);
        return Ok(roomDeviceResource);
    }

    [HttpGet("room/{roomId:int}")]
    [SwaggerOperation(
        Summary = "Gets all devices in a specific room",
        Description = "Get all devices assigned to a specific room.",
        OperationId = "GetRoomDevicesByRoomId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room devices found", typeof(IEnumerable<RoomDeviceResource>))]
    public async Task<IActionResult> GetRoomDevicesByRoomId([FromRoute] int roomId)
    {
        var getRoomDevicesByRoomIdQuery = new GetRoomDevicesByRoomIdQuery(roomId);
        var roomDevices = await roomDeviceQueryService.Handle(getRoomDevicesByRoomIdQuery);
        var roomDeviceResources = roomDevices.Select(RoomDeviceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomDeviceResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new room device assignment",
        Description = "Assigns an IoT device to a specific room.",
        OperationId = "CreateRoomDevice")]
    [SwaggerResponse(StatusCodes.Status201Created, "Room device created successfully", typeof(RoomDeviceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid room device data")]
    public async Task<IActionResult> CreateRoomDevice([FromBody] CreateRoomDeviceResource resource)
    {
        var createRoomDeviceCommand = CreateRoomDeviceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var roomDevice = await roomDeviceCommandService.Handle(createRoomDeviceCommand);
        if (roomDevice is null) return BadRequest("Room device could not be created.");
        var roomDeviceResource = RoomDeviceResourceFromEntityAssembler.ToResourceFromEntity(roomDevice);
        return CreatedAtAction(nameof(GetRoomDeviceById), new { roomDeviceId = roomDevice.Id }, roomDeviceResource);
    }

    [HttpDelete("{roomDeviceId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a room device assignment",
        Description = "Removes an IoT device assignment from a room.",
        OperationId = "DeleteRoomDevice")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Room device deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device not found")]
    public async Task<IActionResult> DeleteRoomDevice([FromRoute] int roomDeviceId)
    {
        var deleteRoomDeviceCommand = new DeleteRoomDeviceCommand(roomDeviceId);
        var result = await roomDeviceCommandService.Handle(deleteRoomDeviceCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}
