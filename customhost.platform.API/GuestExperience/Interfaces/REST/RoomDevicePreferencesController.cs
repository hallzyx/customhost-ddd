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
[SwaggerTag("Available Room Device Preference Endpoints")]
public class RoomDevicePreferencesController(
    IRoomDevicePreferenceCommandService roomDevicePreferenceCommandService,
    IRoomDevicePreferenceQueryService roomDevicePreferenceQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all room device preferences",
        Description = "Get all room device preferences.",
        OperationId = "GetAllRoomDevicePreferences")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preferences found", typeof(IEnumerable<RoomDevicePreferenceResource>))]
    public async Task<IActionResult> GetAllRoomDevicePreferences()
    {
        var getAllRoomDevicePreferencesQuery = new GetAllRoomDevicePreferencesQuery();
        var preferences = await roomDevicePreferenceQueryService.Handle(getAllRoomDevicePreferencesQuery);
        var preferenceResources = preferences.Select(RoomDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(preferenceResources);
    }

    [HttpGet("{preferenceId:int}")]
    [SwaggerOperation(
        Summary = "Gets a room device preference by its ID",
        Description = "Get a room device preference by given preference ID.",
        OperationId = "GetRoomDevicePreferenceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preference found", typeof(RoomDevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device preference not found")]
    public async Task<IActionResult> GetRoomDevicePreferenceById([FromRoute] int preferenceId)
    {
        var getRoomDevicePreferenceByIdQuery = new GetRoomDevicePreferenceByIdQuery(preferenceId);
        var preference = await roomDevicePreferenceQueryService.Handle(getRoomDevicePreferenceByIdQuery);
        if (preference is null) return NotFound();
        var preferenceResource = RoomDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return Ok(preferenceResource);
    }

    [HttpGet("room-device/{roomDeviceId:int}")]
    [SwaggerOperation(
        Summary = "Gets preferences for a specific room device",
        Description = "Get all preferences for a specific room device.",
        OperationId = "GetRoomDevicePreferencesByRoomDeviceId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preferences found", typeof(IEnumerable<RoomDevicePreferenceResource>))]
    public async Task<IActionResult> GetRoomDevicePreferencesByRoomDeviceId([FromRoute] int roomDeviceId)
    {
        var getRoomDevicePreferencesByRoomDeviceIdQuery = new GetRoomDevicePreferencesByRoomDeviceIdQuery(roomDeviceId);
        var preferences = await roomDevicePreferenceQueryService.Handle(getRoomDevicePreferencesByRoomDeviceIdQuery);
        var preferenceResources = preferences.Select(RoomDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(preferenceResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates new room device preferences",
        Description = "Creates preferences for a specific room device.",
        OperationId = "CreateRoomDevicePreference")]
    [SwaggerResponse(StatusCodes.Status201Created, "Room device preference created successfully", typeof(RoomDevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid room device preference data")]
    public async Task<IActionResult> CreateRoomDevicePreference([FromBody] CreateRoomDevicePreferenceResource resource)
    {
        var createRoomDevicePreferenceCommand = CreateRoomDevicePreferenceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var preference = await roomDevicePreferenceCommandService.Handle(createRoomDevicePreferenceCommand);
        if (preference is null) return BadRequest("Room device preference could not be created.");
        var preferenceResource = RoomDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return CreatedAtAction(nameof(GetRoomDevicePreferenceById), new { preferenceId = preference.Id }, preferenceResource);
    }

    [HttpPut("{preferenceId:int}")]
    [SwaggerOperation(
        Summary = "Updates room device preferences",
        Description = "Updates existing room device preferences.",
        OperationId = "UpdateRoomDevicePreference")]
    [SwaggerResponse(StatusCodes.Status200OK, "Room device preference updated successfully", typeof(RoomDevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room device preference not found")]
    public async Task<IActionResult> UpdateRoomDevicePreference([FromRoute] int preferenceId, [FromBody] UpdateRoomDevicePreferenceResource resource)
    {
        var updateRoomDevicePreferenceCommand = new UpdateRoomDevicePreferenceCommand(preferenceId, resource.RoomDeviceId, resource.Preferences);
        var preference = await roomDevicePreferenceCommandService.Handle(updateRoomDevicePreferenceCommand);
        if (preference is null) return NotFound();
        var preferenceResource = RoomDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return Ok(preferenceResource);
    }
}
