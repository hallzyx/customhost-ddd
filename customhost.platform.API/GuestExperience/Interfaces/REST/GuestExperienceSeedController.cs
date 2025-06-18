using System.Net.Mime;
using customhost_backend.GuestExperience.Domain.Model.Aggregates;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace customhost_backend.GuestExperience.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Data Seeding Endpoints for Testing")]
public class GuestExperienceSeedController(
    IIoTDeviceRepository iotDeviceRepository,
    IRoomDeviceRepository roomDeviceRepository,
    IRoomDevicePreferenceRepository roomDevicePreferenceRepository,
    IUserDevicePreferenceRepository userDevicePreferenceRepository,
    IUnitOfWork unitOfWork
) : ControllerBase
{
    [HttpPost("seed")]
    [SwaggerOperation(
        Summary = "Seeds the database with test data",
        Description = "Populates the database with IoT devices, room assignments, and preferences based on the frontend's db.json structure.",
        OperationId = "SeedGuestExperienceData")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data seeded successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Seeding failed")]
    public async Task<IActionResult> SeedGuestExperienceData()
    {
        try
        {
            // Check if data already exists
            var existingDevices = await iotDeviceRepository.ListAsync();
            if (existingDevices.Any())
            {
                return BadRequest("Database already contains IoT devices. Clear the database first or use the reset endpoint.");
            }

            // Create IoT Devices based on db.json
            var iotDevices = new List<IoTDevice>
            {
                new IoTDevice("Temperature Sensor", "climate", 
                    """{"unit": ["Celsius", "Fahrenheit"], "currentValue": "number"}"""),
                new IoTDevice("Smart Light", "lighting", 
                    """{"brightness": "number", "color": ["white", "yellow", "blue"]}"""),
                new IoTDevice("Automatic Curtains", "window-control", 
                    """{"state": ["open", "close"], "position": "number"}"""),
                new IoTDevice("Smart TV", "entertainment", 
                    """{"apps": ["Netflix", "HBO", "YouTube", "Disney+"], "volume": "number", "power": ["on", "off"]}"""),
                new IoTDevice("Room Camera", "security", 
                    """{"resolution": ["720p", "1080p", "4K"], "recording": ["on", "off"]}"""),
                new IoTDevice("Ambient Sound System", "audio", 
                    """{"volume": "number", "source": ["jazz", "hip hop", "classical", "pop", "nature"], "power": ["on", "off"]}"""),
                new IoTDevice("Smart Fragrance Diffuser", "wellness", 
                    """{"intensity": "number", "scent": ["lavender", "vanilla", "citrus", "eucalyptus"], "power": ["on", "off"]}""")
            };

            // Add IoT devices to repository
            foreach (var device in iotDevices)
            {
                await iotDeviceRepository.AddAsync(device);
            }
            await unitOfWork.CompleteAsync();

            // Create Room Device assignments
            var roomDevices = new List<RoomDevice>
            {
                new RoomDevice(1, 1, "working"),  // Room 1 - Temperature Sensor
                new RoomDevice(2, 2, "working"),  // Room 2 - Smart Light
                new RoomDevice(1, 2, "maintenance"), // Room 1 - Smart Light (maintenance)
                new RoomDevice(3, 5, "working"),  // Room 3 - Room Camera
                new RoomDevice(3, 3, "inactive"), // Room 3 - Automatic Curtains (inactive)
                new RoomDevice(4, 1, "inactive"), // Room 4 - Temperature Sensor (inactive)
                new RoomDevice(4, 2, "inactive"), // Room 4 - Smart Light (inactive)
                new RoomDevice(4, 4, "working")   // Room 4 - Smart TV
            };

            foreach (var roomDevice in roomDevices)
            {
                await roomDeviceRepository.AddAsync(roomDevice);
            }
            await unitOfWork.CompleteAsync();

            // Create Room Device Preferences
            var roomDevicePreferences = new List<RoomDevicePreference>
            {
                new RoomDevicePreference(1, """{"unit": "Celsius", "currentValue": 22}"""),
                new RoomDevicePreference(2, """{"color": "yellow", "brightness": 98}"""),
                new RoomDevicePreference(3, """{"brightness": 68, "color": "blue"}"""),
                new RoomDevicePreference(4, """{"resolution": "1080p", "recording": "on"}"""),
                new RoomDevicePreference(5, """{"state": "open", "position": 80}"""),
                new RoomDevicePreference(6, """{"unit": "Celsius", "currentValue": 20}"""),
                new RoomDevicePreference(7, """{"brightness": 78, "color": "white"}"""),
                new RoomDevicePreference(8, """{"apps": "HBO", "volume": 60, "power": "on"}""")
            };

            foreach (var preference in roomDevicePreferences)
            {
                await roomDevicePreferenceRepository.AddAsync(preference);
            }
            await unitOfWork.CompleteAsync();

            // Create User Device Preferences
            var userDevicePreferences = new List<UserDevicePreference>
            {
                new UserDevicePreference(1, 2, "My Reading Light", """{"brightness": 85, "color": "cool"}"""),
                new UserDevicePreference(1, 1, "Room Temperature Control", """{"unit": "Celsius", "currentValue": 24}""")
            };

            foreach (var userPreference in userDevicePreferences)
            {
                await userDevicePreferenceRepository.AddAsync(userPreference);
            }
            await unitOfWork.CompleteAsync();

            return Ok(new { 
                message = "Guest Experience data seeded successfully",
                iotDevices = iotDevices.Count,
                roomDevices = roomDevices.Count,
                roomDevicePreferences = roomDevicePreferences.Count,
                userDevicePreferences = userDevicePreferences.Count
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Seeding failed: {ex.Message}");
        }
    }

    [HttpDelete("reset")]
    [SwaggerOperation(
        Summary = "Resets all Guest Experience data",
        Description = "Removes all IoT devices, room assignments, and preferences from the database.",
        OperationId = "ResetGuestExperienceData")]
    [SwaggerResponse(StatusCodes.Status200OK, "Data reset successfully")]
    public async Task<IActionResult> ResetGuestExperienceData()
    {
        try
        {
            // Remove all data in proper order (to respect foreign key constraints)
            var userPreferences = await userDevicePreferenceRepository.ListAsync();
            foreach (var preference in userPreferences)
            {
                userDevicePreferenceRepository.Remove(preference);
            }

            var roomPreferences = await roomDevicePreferenceRepository.ListAsync();
            foreach (var preference in roomPreferences)
            {
                roomDevicePreferenceRepository.Remove(preference);
            }

            var roomDevices = await roomDeviceRepository.ListAsync();
            foreach (var device in roomDevices)
            {
                roomDeviceRepository.Remove(device);
            }

            var iotDevices = await iotDeviceRepository.ListAsync();
            foreach (var device in iotDevices)
            {
                iotDeviceRepository.Remove(device);
            }

            await unitOfWork.CompleteAsync();

            return Ok(new { message = "Guest Experience data reset successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest($"Reset failed: {ex.Message}");
        }
    }
}
