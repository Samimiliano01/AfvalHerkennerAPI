using Microsoft.AspNetCore.Mvc;
using Sensoring_API.ApiKeyAuth;
using Sensoring_API.Dto;
using Sensoring_API.Repositories;
// For authentication and authorization attributes
// For controller base and HTTP action results
// Data Transfer Objects used in API requests/responses

// Repository interfaces and implementations

namespace Sensoring_API.Controllers;

/// <summary>
/// Provides endpoints for managing litter-related data, including creation, retrieval, and deletion of litter records.
/// </summary>
/// <remarks>
/// This controller contains actions to handle CRUD operations on litter data.
/// Authentication and authorization are required for accessing its endpoints.
/// </remarks>
[ApiController]                           // Enable API-specific features like automatic model validation
[Route("litters")]                      // Route prefix for this controller (endpoint path will start with /Litter)
public class LitterController(ILitterRepository litterRepository) : ControllerBase
{
    /// <summary>
    /// Creates a new litter record using the specified data provided in the request body.
    /// </summary>
    /// <param name="litterCreateDto">
    /// An object containing the litter data to be created.
    /// </param>
    /// <returns>
    /// An HTTP response indicating the result of the operation. Returns a status code of 200 with a success message if the creation is successful.
    /// Returns a status code of 500 with an error message if an unexpected exception occurs during the process.
    /// </returns>
    [AdminApiKey]
    [HttpPost]
    public async Task<ActionResult> Create(LitterCreateDto litterCreateDto)
    {
        try
        {
            // Create a new Litter record and return a 200 status code ok.
            await litterRepository.Create(litterCreateDto);
            return Ok(new { message = "Litter record created successfully" });
        }
        catch (Exception ex)
        {
            // return 500 response code if there is an exception.
            return StatusCode(500, $"An error occurred while creating litter: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves litter records based on the specified query parameters, including ID, date range, and trash type.
    /// </summary>
    /// <param name="id">
    /// An optional ID used to filter the records. If specified, only records with an ID greater than this value will be returned.
    /// </param>
    /// <param name="from">
    /// An optional starting date used to filter the records. Only records created on or after this date will be included.
    /// </param>
    /// <param name="to">
    /// An optional ending date used to filter the records. Only records created on or before this date will be included.
    /// </param>
    /// <param name="trashType">
    /// An optional string used to filter records by the type of trash. Only records matching the specified trash type will be included.
    /// </param>
    /// <returns>
    /// An HTTP response containing a list of litter records that match the query parameters. Returns a status code of 200 with the results if successful.
    /// If no records are found, a 404 status code with an appropriate message is returned.
    /// In case of an error, a status code of 500 with an error message is returned.
    /// </returns>
    [HttpGet]
    [UserApiKey]
    public async Task<ActionResult<LitterReadDto>> Read([FromQuery] int? id, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string? trashType)
    {
        try
        {
            // Retrieve all litter records from repo
            var result = (await litterRepository.Read())?.AsEnumerable();  
            
            // check if no records found
            if (result == null)
            {
                return NotFound("No litter records found.");
            }

            // Filter by Id if provided
            if (id != null)
            {
                result = result.Where(l => l.Id > id);
            }

            // Filter by start date if provided
            if (from != null)
            {
                result = result.Where(l => l.Time >= from);
            }
            
            // Filter by end date if provided
            if (to != null)
            {
                result = result.Where(l => l.Time <= to);
            }
            
            // Filter by trash type if provided
            if (!string.IsNullOrEmpty(trashType))
            {
                result = result.Where(l => l.TypeOfTrash.Equals(trashType, StringComparison.OrdinalIgnoreCase));
            }
            
            // Return the list of litter records as JSON
            return Ok(result);
        }
        catch (Exception ex)
        {
            // return 500 response code if there is an exception.
            return StatusCode(500, $"An error occurred while retrieving litter: {ex.Message}");
        }
    }
    
    [AdminApiKey]
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] int id)
    {
        // Validate that the ID is not negative
        if (id < 0)                   
        {
            return BadRequest("Invalid litter ID.");
        }

        try
        {
            // Call repo to delete litter record by ID and return a 200 ok status code.
            await litterRepository.Delete(id);
            return Ok(new { message = "Litter record deleted successfully." });
        }
        // Catch case when litter with given ID does not exist
        catch (KeyNotFoundException knf)  
        {
            // Return 404 Not Found with error message
            return NotFound(knf.Message);  
        }
        // Catch any other unexpected errors during deletion
        catch (Exception ex)              
        {
            // return 500 response code if there is an exception.
            return StatusCode(500, $"An error occurred while deleting litter: {ex.Message}");
        }
    }
}