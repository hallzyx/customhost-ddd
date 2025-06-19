using System.Net.Mime;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Interfaces.REST.Resources;
using customhost_backend.crm.Interfaces.REST.Transform;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.GuestExperience.Domain.Model.Queries;
using customhost_backend.GuestExperience.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.crm.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Room Endpoints.")]
public class RoomsController(
    IRoomCommandService roomCommandService,
    IRoomQueryService roomQueryService,
    IRoomDeviceQueryService roomDeviceQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Rooms", "Get all rooms with optional filtering by hotel.", OperationId = "GetAllRooms")]
    [SwaggerResponse(200, "The rooms were found and returned.", typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRooms([FromQuery] int? hotelId = null)
    {
        IEnumerable<customhost_backend.crm.Domain.Models.Aggregates.Room> rooms;
        
        if (hotelId.HasValue)
        {
            rooms = await roomQueryService.GetByHotelIdAsync(hotelId.Value);
        }
        else
        {
            rooms = await roomQueryService.GetAllAsync();
        }

        var roomResources = RoomResourceFromEntityAssembler.ToResourceFromEntity(rooms);
        return Ok(roomResources);
    }

    [HttpGet("{roomId:int}")]
    [SwaggerOperation("Get Room by Id", "Get a room by its unique identifier.", OperationId = "GetRoomById")]
    [SwaggerResponse(200, "The room was found and returned.", typeof(RoomResource))]
    [SwaggerResponse(404, "The room was not found.")]
    public async Task<IActionResult> GetRoomById(int roomId)
    {
        var room = await roomQueryService.GetByIdAsync(roomId);
        if (room is null) return NotFound();
        
        var roomResource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(roomResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Room", "Create a new room.", OperationId = "CreateRoom")]
    [SwaggerResponse(201, "The room was created.", typeof(RoomResource))]
    [SwaggerResponse(400, "The room was not created.")]
    public async Task<IActionResult> CreateRoom(CreateRoomResource resource)
    {
        var createRoomCommand = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(resource);
        var room = await roomCommandService.Handle(createRoomCommand);
        if (room is null) return BadRequest("Room number already exists in this hotel.");
        
        var roomResource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return CreatedAtAction(nameof(GetRoomById), new { roomId = room.Id }, roomResource);
    }

    [HttpPut("{roomId:int}")]
    [SwaggerOperation("Update Room", "Update an existing room.", OperationId = "UpdateRoom")]
    [SwaggerResponse(200, "The room was updated.", typeof(RoomResource))]
    [SwaggerResponse(404, "The room was not found.")]
    [SwaggerResponse(400, "The room could not be updated.")]
    public async Task<IActionResult> UpdateRoom(int roomId, UpdateRoomResource resource)
    {
        var updateRoomCommand = UpdateRoomCommandFromResourceAssembler.ToCommandFromResource(roomId, resource);
        var room = await roomCommandService.Handle(updateRoomCommand);
        if (room is null) return NotFound();
        
        var roomResource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(roomResource);
    }

    [HttpDelete("{roomId:int}")]
    [SwaggerOperation("Delete Room", "Delete a room by its unique identifier.", OperationId = "DeleteRoom")]
    [SwaggerResponse(204, "The room was deleted successfully.")]
    [SwaggerResponse(404, "The room was not found.")]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        var deleteRoomCommand = new DeleteRoomCommand(roomId);
        var success = await roomCommandService.Handle(deleteRoomCommand);
        if (!success) return NotFound();
        
        return NoContent();
    }

    [HttpGet("with-devices")]
    [SwaggerOperation("Get All Rooms with IoT Devices", "Get all rooms with their associated IoT devices.", OperationId = "GetAllRoomsWithDevices")]
    [SwaggerResponse(200, "The rooms with devices were found and returned.", typeof(IEnumerable<RoomWithDevicesResource>))]
    public async Task<IActionResult> GetAllRoomsWithDevices([FromQuery] int? hotelId = null)
    {
        IEnumerable<customhost_backend.crm.Domain.Models.Aggregates.Room> rooms;
        
        if (hotelId.HasValue)
        {
            rooms = await roomQueryService.GetByHotelIdAsync(hotelId.Value);
        }
        else
        {
            rooms = await roomQueryService.GetAllAsync();
        }

        // Get all room devices
        var getAllRoomDevicesQuery = new GetAllRoomDevicesQuery();
        var allRoomDevices = await roomDeviceQueryService.Handle(getAllRoomDevicesQuery);

        // Create rooms with devices resources
        var roomsWithDevices = rooms.Select(room =>
        {
            // Filter room devices for this specific room
            var roomDevices = allRoomDevices.Where(rd => rd.RoomId == room.Id);
            
            // Convert to resources
            var deviceResources = roomDevices.Select(RoomDeviceResourceFromEntityAssembler.ToResourceFromEntity);
              return new RoomWithDevicesResource(
                room.Id,
                room.RoomNumber,
                room.Status.ToString(),
                room.Type.ToString(),
                room.HotelId,
                room.Price,
                room.Floor,
                deviceResources
            );
        });

        return Ok(roomsWithDevices);
    }
}