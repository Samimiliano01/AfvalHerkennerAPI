using Microsoft.AspNetCore.Mvc;
using Sensoring_API.Dto;
using Sensoring_API.Repositories;

namespace Sensoring_API.Controllers;

public class LitterController(ILitterRepository litterRepository) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(LitterCreateDto litterCreateDto)
    {
        try
        {
            await litterRepository.Create(litterCreateDto);
        }
        catch (Exception)
        {
            return Problem();
        }
        return Created();
    }
    [HttpGet]
    public async Task<ActionResult<LitterReadDto>> Read()
    {
        try
        {
            var result = await litterRepository.Read();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] int id)
    {
        try
        {
            await litterRepository.Delete(id);
        }
        catch (Exception)
        {
            return Problem();
        }
        return NoContent();
    }
}