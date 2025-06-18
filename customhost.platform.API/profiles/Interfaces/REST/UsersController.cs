using System.Net.Mime;
using customhost_backend.profiles.Domain.Models.Commands;
using customhost_backend.profiles.Domain.Models.ValueObjects;
using customhost_backend.profiles.Domain.Services;
using customhost_backend.profiles.Interfaces.REST.Resources;
using customhost_backend.profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Users")]
public class UsersController(
    IUserCommandService userCommandService,
    IUserQueryService userQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new user",
        Description = "Creates a new user with the specified details.",
        OperationId = "CreateUser")]
    [SwaggerResponse(201, "User created successfully", typeof(UserResource))]
    [SwaggerResponse(400, "User can't be created.", null)]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserResource userResource)
    {
        var command = CreateUserCommandFromResourceAssembler.ToCommandFromResource(userResource);
        var result = await userCommandService.Handle(command);
        if (result == null)
            return BadRequest("User could not be created. Email might already exist.");

        return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, 
            UserResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all users",
        Description = "Retrieves a list of all users.",
        OperationId = "GetUsers")]
    [SwaggerResponse(200, "Users retrieved successfully", typeof(IEnumerable<UserResource>))]
    public async Task<ActionResult> GetUsers()
    {
        var users = (await userQueryService.GetAllAsync()).ToList();
        var resources = UserResourceFromEntityAssembler.ToResourcesFromEntities(users);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get user by ID",
        Description = "Retrieves a specific user by their ID.",
        OperationId = "GetUserById")]
    [SwaggerResponse(200, "User retrieved successfully", typeof(UserResource))]
    [SwaggerResponse(404, "User not found", null)]
    public async Task<ActionResult> GetUserById(int id)
    {
        var user = await userQueryService.GetByIdAsync(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");

        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

    [HttpGet("email/{email}")]
    [SwaggerOperation(
        Summary = "Get user by email",
        Description = "Retrieves a specific user by their email address.",
        OperationId = "GetUserByEmail")]
    [SwaggerResponse(200, "User retrieved successfully", typeof(UserResource))]
    [SwaggerResponse(404, "User not found", null)]
    public async Task<ActionResult> GetUserByEmail(string email)
    {
        var user = await userQueryService.GetByEmailAsync(email);
        if (user == null)
            return NotFound($"User with email {email} not found.");

        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

    [HttpGet("hotel/{hotelId:int}")]
    [SwaggerOperation(
        Summary = "Get users by hotel ID",
        Description = "Retrieves all users for a specific hotel.",
        OperationId = "GetUsersByHotelId")]
    [SwaggerResponse(200, "Users retrieved successfully", typeof(IEnumerable<UserResource>))]
    public async Task<ActionResult> GetUsersByHotelId(int hotelId)
    {
        var users = (await userQueryService.GetByHotelIdAsync(hotelId)).ToList();
        var resources = UserResourceFromEntityAssembler.ToResourcesFromEntities(users);
        return Ok(resources);
    }

    [HttpGet("role/{role}")]
    [SwaggerOperation(
        Summary = "Get users by role",
        Description = "Retrieves all users with a specific role.",
        OperationId = "GetUsersByRole")]
    [SwaggerResponse(200, "Users retrieved successfully", typeof(IEnumerable<UserResource>))]
    [SwaggerResponse(400, "Invalid role value", null)]
    public async Task<ActionResult> GetUsersByRole(string role)
    {
        if (!Enum.TryParse<EUserRole>(role, true, out var roleEnum))
            return BadRequest($"Invalid role value: {role}");

        var users = (await userQueryService.GetByRoleAsync(roleEnum)).ToList();
        var resources = UserResourceFromEntityAssembler.ToResourcesFromEntities(users);
        return Ok(resources);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update a user",
        Description = "Updates an existing user with new information.",
        OperationId = "UpdateUser")]
    [SwaggerResponse(200, "User updated successfully", typeof(UserResource))]
    [SwaggerResponse(404, "User not found", null)]
    [SwaggerResponse(400, "User update failed", null)]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserResource updateResource)
    {
        var command = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(id, updateResource);
        var result = await userCommandService.Handle(command);
        if (result == null)
            return NotFound($"User with ID {id} not found or email already exists.");

        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Delete user",
        Description = "Deletes a user from the system.",
        OperationId = "DeleteUser")]
    [SwaggerResponse(200, "User deleted successfully")]
    [SwaggerResponse(404, "User not found", null)]
    [SwaggerResponse(400, "User deletion failed", null)]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var command = new DeleteUserCommand(id);
        var result = await userCommandService.Handle(command);
        if (!result)
            return NotFound($"User with ID {id} not found.");

        return Ok($"User with ID {id} deleted successfully.");
    }
}
