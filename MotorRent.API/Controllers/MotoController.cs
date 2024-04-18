using Microsoft.AspNetCore.Mvc;
using MotorRent.DeliveryManagement.Application.Services;
using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.API.Controllers;

[Route("api/moto")]
[ApiController]
public class MotoController : ControllerBase
{
    private readonly MotoService _motoService;

    public MotoController(MotoService motoService)
    {
        _motoService = motoService;
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMotoById([FromQuery] int id)
    {
        var moto = await _motoService.GetMotoByIdAsync(id);
        if (moto == null) return NotFound();
        return Ok(moto);
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllMotos()
    {
        try
        {
            var motos = await _motoService.GetAllMotosAsync();
            return Ok(motos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateMoto([FromBody] Moto moto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            await _motoService.AddMoto(moto);
            return CreatedAtAction(nameof(GetMotoById), new { id = moto.Id }, moto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateMoto([FromBody] Moto moto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            moto.Id = moto.Id;
            var result = await _motoService.UpdateMotoAsync(moto);
            if (!result.Success) return NotFound(result.Message);

            return CreatedAtAction(nameof(GetMotoById), new { id = moto.Id }, moto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveMoto(int id)
    {
        try
        {
            var result = await _motoService.RemoveMotoAsync(id);
            if (!result.Success) return NotFound(result.Message);

            return Ok(result.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
