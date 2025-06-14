using Microsoft.AspNetCore.Mvc;
using Sensoring_API.ApiKeyAuth;
using Sensoring_API.Dto;
using Sensoring_API.Repositories;
// For authentication and authorization attributes
// For controller base and HTTP action results
// Data Transfer Objects used in API requests/responses

// Repository interfaces and implementations

namespace Sensoring_API.Controllers;

[ApiController]                           // Enable API-specific features like automatic model validation
[Route("litters")]                      // Route prefix for this controller (endpoint path will start with /Litter)
public class LitterController(ILitterRepository litterRepository) : ControllerBase
{
    [AdminApiKey]
    [HttpPost]                          // Handles HTTP POST requests to create a litter record
    public async Task<ActionResult> Create(LitterCreateDto litterCreateDto)
    {
        try
        {
            await litterRepository.Create(litterCreateDto);      // Call repo to add new litter record
            return Ok(new { message = "Litter record created successfully" });  // Return success message in JSON
        }
        catch (Exception ex)              // Catch any unexpected errors during creation
        {
            // Return HTTP 500 with error message on failure
            return StatusCode(500, $"An error occurred while creating litter: {ex.Message}");
        }
    }

    [HttpGet] // Handles HTTP GET requests to read all litter records
    [UserApiKey]
    public async Task<ActionResult<LitterReadDto>> Read([FromQuery] int? id, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string? trashType)
    {
        try
        {
            var result = (await litterRepository.Read())?.AsEnumerable();         // Retrieve all litter records from repo
            if (result == null)            // Check if no records found
            {
                return NotFound("No litter records found.");    // Return 404 with message if empty
            }

            if (id != null)
            {
                result = result.Where(l => l.Id > id);  // Filter by ID if provided
            }

            if (from != null)
            {
                result = result.Where(l => l.Time >= from);  // Filter by start date if provided
            }
            
            if (to != null)
            {
                result = result.Where(l => l.Time <= to);  // Filter by end date if provided
            }
            
            if (!string.IsNullOrEmpty(trashType))
            {
                result = result.Where(l => l.TypeOfTrash.Equals(trashType, StringComparison.OrdinalIgnoreCase));  // Filter by trash type if provided
            }
            
            return Ok(result);                                    // Return the list of litter records as JSON
        }
        catch (Exception ex)              // Catch any unexpected errors during retrieval
        {
            // Return HTTP 500 with error message on failure
            return StatusCode(500, $"An error occurred while retrieving litter: {ex.Message}");
        }
    }
    
    [AdminApiKey]
    [HttpDelete]                      // Handles HTTP DELETE requests to delete a litter record by ID
    public async Task<ActionResult> Delete([FromQuery] int id)
    {
        if (id < 0)                   // Validate that the ID is not negative
        {
            return BadRequest("Invalid litter ID.");  // Return 400 Bad Request for invalid ID
        }

        try
        {
            await litterRepository.Delete(id);         // Call repo to delete litter record by ID
            return Ok(new { message = "Litter record deleted successfully." });  // Return success message
        }
        catch (KeyNotFoundException knf)  // Catch case when litter with given ID does not exist
        {
            return NotFound(knf.Message);  // Return 404 Not Found with error message
        }
        catch (Exception ex)              // Catch any other unexpected errors during deletion
        {
            // Return HTTP 500 with error message on failure
            return StatusCode(500, $"An error occurred while deleting litter: {ex.Message}");
        }
    }
}