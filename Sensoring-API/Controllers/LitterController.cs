using Microsoft.AspNetCore.Authorization;  // For authentication and authorization attributes
using Microsoft.AspNetCore.Mvc;             // For controller base and HTTP action results
using Sensoring_API.Dto;                    // Data Transfer Objects used in API requests/responses
using Sensoring_API.Repositories;           // Repository interfaces and implementations

namespace Sensoring_API.Controllers;

[Authorize]                               // Require authorization for all endpoints in this controller
[ApiController]                           // Enable API-specific features like automatic model validation
[Route("Litter")]                      // Route prefix for this controller (endpoint path will start with /Litter)
public class LitterController(ILitterRepository litterRepository) : ControllerBase
{
    [Authorize(Roles = "Admin")]         // Only Admin role can access this POST endpoint
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
    
    [HttpGet]                          // Handles HTTP GET requests to read all litter records
    public async Task<ActionResult<LitterReadDto>> Read()
    {
        try
        {
            var result = await litterRepository.Read();         // Retrieve all litter records from repo
            if (result == null)            // Check if no records found
            {
                return NotFound("No litter records found.");    // Return 404 with message if empty
            }
            return Ok(result);                                    // Return the list of litter records as JSON
        }
        catch (Exception ex)              // Catch any unexpected errors during retrieval
        {
            // Return HTTP 500 with error message on failure
            return StatusCode(500, $"An error occurred while retrieving litter: {ex.Message}");
        }
    }
    
    [Authorize(Roles = "Admin")]       // Only Admin role can access this DELETE endpoint
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