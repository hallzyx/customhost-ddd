using customhost_backend.crm.Domain.Models.Commands;
using customhost_backend.crm.Domain.Models.Queries;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Interfaces.REST.Resources;
using customhost_backend.crm.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace customhost_backend.crm.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class StaffMembersController(
    IStaffMemberCommandService staffMemberCommandService,
    IStaffMemberQueryService staffMemberQueryService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateStaffMember([FromBody] CreateStaffMemberResource resource)
    {
        try
        {
            var createStaffMemberCommand = CreateStaffMemberCommandFromResourceAssembler.ToCommandFromResource(resource);
            var staffMember = await staffMemberCommandService.Handle(createStaffMemberCommand);
            
            if (staffMember is null) return BadRequest("Failed to create staff member");
            
            var staffMemberResource = StaffMemberResourceFromEntityAssembler.ToResourceFromEntity(staffMember);
            return CreatedAtAction(nameof(GetStaffMemberById), new { staffMemberId = staffMember.Id }, staffMemberResource);
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred while creating the staff member: {e.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStaffMembers()
    {
        var getAllStaffMembersQuery = new GetAllStaffMembersQuery();
        var staffMembers = await staffMemberQueryService.Handle(getAllStaffMembersQuery);
        var staffMemberResources = staffMembers.Select(StaffMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffMemberResources);
    }

    [HttpGet("{staffMemberId:int}")]
    public async Task<IActionResult> GetStaffMemberById(int staffMemberId)
    {
        var getStaffMemberByIdQuery = new GetStaffMemberByIdQuery(staffMemberId);
        var staffMember = await staffMemberQueryService.Handle(getStaffMemberByIdQuery);
        
        if (staffMember == null) return NotFound($"Staff member with id {staffMemberId} not found");
        
        var staffMemberResource = StaffMemberResourceFromEntityAssembler.ToResourceFromEntity(staffMember);
        return Ok(staffMemberResource);
    }

    [HttpGet("hotel/{hotelId:int}")]
    public async Task<IActionResult> GetStaffMembersByHotelId(int hotelId)
    {
        var getStaffMembersByHotelIdQuery = new GetStaffMembersByHotelIdQuery(hotelId);
        var staffMembers = await staffMemberQueryService.Handle(getStaffMembersByHotelIdQuery);
        var staffMemberResources = staffMembers.Select(StaffMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffMemberResources);
    }

    [HttpGet("hotel/{hotelId:int}/department/{department}")]
    public async Task<IActionResult> GetStaffMembersByDepartment(int hotelId, string department)
    {
        var getStaffMembersByDepartmentQuery = new GetStaffMembersByDepartmentQuery(hotelId, department);
        var staffMembers = await staffMemberQueryService.Handle(getStaffMembersByDepartmentQuery);
        var staffMemberResources = staffMembers.Select(StaffMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffMemberResources);
    }

    [HttpGet("hotel/{hotelId:int}/active")]
    public async Task<IActionResult> GetActiveStaffMembers(int hotelId)
    {
        var getActiveStaffMembersQuery = new GetActiveStaffMembersQuery(hotelId);
        var staffMembers = await staffMemberQueryService.Handle(getActiveStaffMembersQuery);
        var staffMemberResources = staffMembers.Select(StaffMemberResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffMemberResources);
    }

    [HttpPut("{staffMemberId:int}")]
    public async Task<IActionResult> UpdateStaffMember(int staffMemberId, [FromBody] UpdateStaffMemberResource resource)
    {
        try
        {
            var updateStaffMemberCommand = UpdateStaffMemberCommandFromResourceAssembler.ToCommandFromResource(staffMemberId, resource);
            var staffMember = await staffMemberCommandService.Handle(updateStaffMemberCommand);
            
            if (staffMember is null) return NotFound($"Staff member with id {staffMemberId} not found");
            
            var staffMemberResource = StaffMemberResourceFromEntityAssembler.ToResourceFromEntity(staffMember);
            return Ok(staffMemberResource);
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred while updating the staff member: {e.Message}");
        }
    }

    [HttpPatch("{staffMemberId:int}/status")]
    public async Task<IActionResult> ChangeStaffMemberStatus(int staffMemberId, [FromBody] ChangeStaffMemberStatusResource resource)
    {
        try
        {
            var changeStatusCommand = ChangeStaffMemberStatusCommandFromResourceAssembler.ToCommandFromResource(staffMemberId, resource);
            var staffMember = await staffMemberCommandService.Handle(changeStatusCommand);
            
            if (staffMember is null) return NotFound($"Staff member with id {staffMemberId} not found");
            
            var staffMemberResource = StaffMemberResourceFromEntityAssembler.ToResourceFromEntity(staffMember);
            return Ok(staffMemberResource);
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred while changing staff member status: {e.Message}");
        }
    }

    [HttpDelete("{staffMemberId:int}")]
    public async Task<IActionResult> DeleteStaffMember(int staffMemberId)
    {
        var deleteStaffMemberCommand = new DeleteStaffMemberCommand(staffMemberId);
        var result = await staffMemberCommandService.Handle(deleteStaffMemberCommand);
        
        if (!result) return NotFound($"Staff member with id {staffMemberId} not found");
        
        return NoContent();
    }
}
