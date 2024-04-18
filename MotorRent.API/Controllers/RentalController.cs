using Microsoft.AspNetCore.Mvc;
using MotorRent.DeliveryManagement.Application.Services;

namespace MotorRent.API.Controllers;

[Route("api/rental")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly RentalService _rentalService;

    public RentalController(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> RentMoto([FromForm] int motoId, [FromForm] int deliverymanId, [FromForm] DateTime startDate, [FromForm] DateTime endDate, [FromForm] DateTime expectedEndDate)
    {
        try
        {
            var totalCost = _rentalService.RentMoto(motoId, deliverymanId, startDate, endDate, expectedEndDate);
            return Ok(totalCost);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }   
}
