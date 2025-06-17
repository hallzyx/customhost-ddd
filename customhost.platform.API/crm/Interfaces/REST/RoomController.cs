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
[Tags("Rooms")]
public class RoomController(
    IRoomCommandService roomCommandService,
    IRoomQueryService roomQueryService)
    : ControllerBase
{
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new room",
        Description = "Creates a new room with the specified details.",
        OperationId = "CreateRoom")]
    [SwaggerResponse(201, "Room created successfully", typeof(Room))]
    [SwaggerResponse(400, "Room can't be created.", null)]
    public async Task<ActionResult> CreateRoom([FromBody] CreateRoomResource roomResource)
    {
        // if(!ModelState.IsValid)
        //     return BadRequest(ModelState);
        
        var command = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(roomResource);
        var result = await roomCommandService.Handle(command);
        if (result == null)
            return BadRequest("There was already a room with the same number in the hotel.");
        return Ok(CreateRoomResourceFromEntityAssembler.ToResourceFromEntity(result));
        var responseResourse = CreateRoomResourceFromEntityAssembler.ToResourceFromEntity(result);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get a rooms",
        Description = "Retrieves a list of rooms.",
        OperationId = "GetRooms")]
    [SwaggerResponse(200, "Rooms retrieved successfully", typeof(IEnumerable<RoomResource>))]
    public async Task<ActionResult> GetRooms()
    {
        var rooms = (await roomQueryService.Handle()).ToList();
        if (rooms.Count == 0)
            return NotFound("No rooms found.");
        

        var resources = CreateRoomResourceFromEntityAssembler.ToResourcesFromEntities(rooms);
        return Ok(rooms);
    }
            
}