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
[SwaggerTag("Available User Device Preference Endpoints")]
public class UserDevicePreferencesController(
    IUserDevicePreferenceCommandService userDevicePreferenceCommandService,
    IUserDevicePreferenceQueryService userDevicePreferenceQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all user device preferences",
        Description = "Get all user device preferences.",
        OperationId = "GetAllUserDevicePreferences")]
    [SwaggerResponse(StatusCodes.Status200OK, "User device preferences found", typeof(IEnumerable<UserDevicePreferenceResource>))]
    public async Task<IActionResult> GetAllUserDevicePreferences()
    {
        var getAllUserDevicePreferencesQuery = new GetAllUserDevicePreferencesQuery();
        var preferences = await userDevicePreferenceQueryService.Handle(getAllUserDevicePreferencesQuery);
        var preferenceResources = preferences.Select(UserDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(preferenceResources);
    }

    [HttpGet("{preferenceId:int}")]
    [SwaggerOperation(
        Summary = "Gets a user device preference by its ID",
        Description = "Get a user device preference by given preference ID.",
        OperationId = "GetUserDevicePreferenceById")]
    [SwaggerResponse(StatusCodes.Status200OK, "User device preference found", typeof(UserDevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User device preference not found")]
    public async Task<IActionResult> GetUserDevicePreferenceById([FromRoute] int preferenceId)
    {
        var getUserDevicePreferenceByIdQuery = new GetUserDevicePreferenceByIdQuery(preferenceId);
        var preference = await userDevicePreferenceQueryService.Handle(getUserDevicePreferenceByIdQuery);
        if (preference is null) return NotFound();
        var preferenceResource = UserDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return Ok(preferenceResource);
    }

    [HttpGet("user/{userId:int}")]
    [SwaggerOperation(
        Summary = "Gets preferences for a specific user",
        Description = "Get all device preferences for a specific user.",
        OperationId = "GetUserDevicePreferencesByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, "User device preferences found", typeof(IEnumerable<UserDevicePreferenceResource>))]
    public async Task<IActionResult> GetUserDevicePreferencesByUserId([FromRoute] int userId)
    {
        var getUserDevicePreferencesByUserIdQuery = new GetUserDevicePreferencesByUserIdQuery(userId);
        var preferences = await userDevicePreferenceQueryService.Handle(getUserDevicePreferencesByUserIdQuery);
        var preferenceResources = preferences.Select(UserDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(preferenceResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates new user device preferences",
        Description = "Creates preferences for a user and specific device.",
        OperationId = "CreateUserDevicePreference")]
    [SwaggerResponse(StatusCodes.Status201Created, "User device preference created successfully", typeof(UserDevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid user device preference data")]
    public async Task<IActionResult> CreateUserDevicePreference([FromBody] CreateUserDevicePreferenceResource resource)
    {
        var createUserDevicePreferenceCommand = CreateUserDevicePreferenceCommandFromResourceAssembler.ToCommandFromResource(resource);
        var preference = await userDevicePreferenceCommandService.Handle(createUserDevicePreferenceCommand);
        if (preference is null) return BadRequest("User device preference could not be created.");
        var preferenceResource = UserDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return CreatedAtAction(nameof(GetUserDevicePreferenceById), new { preferenceId = preference.Id }, preferenceResource);
    }

    [HttpPut("{preferenceId:int}")]
    [SwaggerOperation(
        Summary = "Updates user device preferences",
        Description = "Updates existing user device preferences.",
        OperationId = "UpdateUserDevicePreference")]
    [SwaggerResponse(StatusCodes.Status200OK, "User device preference updated successfully", typeof(UserDevicePreferenceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User device preference not found")]
    public async Task<IActionResult> UpdateUserDevicePreference([FromRoute] int preferenceId, [FromBody] UpdateUserDevicePreferenceResource resource)
    {
        var updateUserDevicePreferenceCommand = new UpdateUserDevicePreferenceCommand(preferenceId, resource.UserId, resource.DeviceId, resource.CustomName, resource.Overrides);
        var preference = await userDevicePreferenceCommandService.Handle(updateUserDevicePreferenceCommand);
        if (preference is null) return NotFound();
        var preferenceResource = UserDevicePreferenceResourceFromEntityAssembler.ToResourceFromEntity(preference);
        return Ok(preferenceResource);
    }

    [HttpDelete("{preferenceId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a user device preference",
        Description = "Deletes an existing user device preference.",
        OperationId = "DeleteUserDevicePreference")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "User device preference deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User device preference not found")]
    public async Task<IActionResult> DeleteUserDevicePreference([FromRoute] int preferenceId)
    {
        var deleteUserDevicePreferenceCommand = new DeleteUserDevicePreferenceCommand(preferenceId);
        var result = await userDevicePreferenceCommandService.Handle(deleteUserDevicePreferenceCommand);
        if (!result) return NotFound();
        return NoContent();
    }
}
