using System.Net.Mime;
using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Interfaces.REST.Resources;
using customhost_backend.crm.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.crm.Interfaces.REST;

[ApiController]

[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Hotel Endpoints.")]
public class HotelController(
    IHotelCommandService hotelCommandService,
    IHotelQueryService hotelQueryService)
: ControllerBase
{
    [HttpGet("{hotelId:int}")]
    [SwaggerOperation("Get Hotel by Id", "Get a hotel by its unique identifier.", OperationId = "GetHotelById")]
    [SwaggerResponse(200, "The hotel was found and returned.", typeof(HotelResource))]
    [SwaggerResponse(404, "The hotel was not found.")]
    public async Task<IActionResult> GetHotelById(int hotelId)
    {
        var getHotelByIdQuery = new GetHotelByIdQuery(hotelId);
        var hotel = await hotelQueryService.Handle(getHotelByIdQuery);
        if (hotel is null) return NotFound();
        var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
        return Ok(hotelResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Hotel", "Create a new hotel.", OperationId = "CreateHotel")]
    [SwaggerResponse(201, "The hotel was created.", typeof(HotelResource))]
    [SwaggerResponse(400, "The hotel was not created.")]
    public async Task<IActionResult> CreateHotel(CreateHotelResource resource)
    {
        var createHotelCommand = CreateHotelCommandFromResourceAssembler.ToCommandFromResource(resource);
        var hotel = await hotelCommandService.Handle(createHotelCommand);
        if (hotel is null) return BadRequest();
        var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
        return CreatedAtAction(nameof(GetHotelById), new { hotelId = hotel.Id }, hotelResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Hotels", "Get all hotels.", OperationId = "GetAllHotels")]
    [SwaggerResponse(200, "The hotels were found and returned.", typeof(IEnumerable<HotelResource>))]
    public async Task<IActionResult> GetAllHotels()
    {
        var getAllHotelsQuery = new GetAllHotelsQuery();
        var hotels = await hotelQueryService.Handle(getAllHotelsQuery);
        var hotelResources = hotels.Select(HotelResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(hotelResources);
    }

    [HttpPut("{hotelId:int}")]
    [SwaggerOperation("Update Hotel", "Update an existing hotel.", OperationId = "UpdateHotel")]
    [SwaggerResponse(200, "The hotel was updated.", typeof(HotelResource))]
    [SwaggerResponse(404, "The hotel was not found.")]
    [SwaggerResponse(400, "The hotel was not updated.")]
    public async Task<IActionResult> UpdateHotel(int hotelId, UpdateHotelResource resource)
    {
        var updateHotelCommand = UpdateHotelCommandFromResourceAssembler.ToCommandFromResource(hotelId, resource);
        var hotel = await hotelCommandService.Handle(updateHotelCommand);
        if (hotel is null) return NotFound();
        var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
        return Ok(hotelResource);
    }

    [HttpDelete("{hotelId:int}")]
    [SwaggerOperation("Delete Hotel", "Delete an existing hotel.", OperationId = "DeleteHotel")]
    [SwaggerResponse(200, "The hotel was deleted.")]
    [SwaggerResponse(404, "The hotel was not found.")]
    public async Task<IActionResult> DeleteHotel(int hotelId)
    {
        var deleteHotelCommand = new DeleteHotelCommand(hotelId);
        var result = await hotelCommandService.Handle(deleteHotelCommand);
        if (!result) return NotFound();
        return Ok("Hotel deleted successfully");
    }

    [HttpGet("admin/{adminId:int}")]
    [SwaggerOperation("Get Hotels by Admin", "Get hotels managed by a specific admin.", OperationId = "GetHotelsByAdmin")]
    [SwaggerResponse(200, "The hotels were found and returned.", typeof(IEnumerable<HotelResource>))]
    public async Task<IActionResult> GetHotelsByAdmin(int adminId)
    {
        var getHotelsByAdminQuery = new GetHotelsByAdminIdQuery(adminId);
        var hotels = await hotelQueryService.Handle(getHotelsByAdminQuery);
        var hotelResources = hotels.Select(HotelResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(hotelResources);
    }
}
