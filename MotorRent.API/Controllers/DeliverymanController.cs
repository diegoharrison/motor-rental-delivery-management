using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MotorRent.DeliveryManagement.Application.Services;
using MotorRent.DeliveryManagement.Core.Entities;

namespace MotorRent.API.Controllers;

[Route("api/deliveryman")]
[ApiController]
public class DeliverymanController : ControllerBase
{
    private readonly DeliverymanService _deliverymanService;
    private readonly IMapper _mapper;

    public DeliverymanController(DeliverymanService deliverymanService, IMapper mapper)
    {
        _deliverymanService = deliverymanService;
        _mapper = mapper;
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeliverymanById([FromQuery] int id)
    {
        var deliveryman = await _deliverymanService.GetDeliverymanByIdAsync(id);
        if (deliveryman == null) return NotFound();
        return Ok(deliveryman);
    }

    [HttpGet("cnpj")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeliverymanByCnpj([FromQuery] string cnpj)
    {
        var deliveryman = await _deliverymanService.GetDeliverymanByCnpjAsync(cnpj);
        if (deliveryman == null) return NotFound();
        return Ok(deliveryman);
    }

    [HttpGet("driverLicenseNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDeliverymanByDriverLicenseNumber([FromQuery] string driverLicenseNumber)
    {
        var deliveryman = await _deliverymanService.GetDeliverymanByDriverLicenseNumberAsync(driverLicenseNumber);
        if (deliveryman == null) return NotFound();
        return Ok(deliveryman);
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllMotos()
    {
        try
        {
            var motos = await _deliverymanService.GetAllDeliverymenAsync();
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
    public async Task<IActionResult> CreateDeliveryman([FromBody] Deliveryman deliveryman)
    {
        try
        {
            //var deliveryMan = _mapper.Map<DeliverymanDto, Deliveryman>(deliverymanDto);

            if (deliveryman.DriverLicenseImage != null && deliveryman.DriverLicenseImage.Length > 0)
            {
                await _deliverymanService.CreateOrUpdateDeliveryManAsync(deliveryman);
            }
            else
            {                
                return BadRequest("Driver license image path is required.");
            }

            return CreatedAtAction(nameof(GetDeliverymanById), new { id = deliveryman.Id }, deliveryman);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}