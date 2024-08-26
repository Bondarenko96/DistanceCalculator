using DistanceCalculatorApi.Extensions;
using DistanceCalculatorApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistanceCalculatorApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DistanceController(ICTeleportService cTeleportService) : ControllerBase
{
    [HttpGet(Name = "Get/{from}/{to}")]
    public async Task<ActionResult<double>> Get(string from, string to)
    {
        if (from.Length != 3 || to.Length != 3)
            return BadRequest("IATA код должен состоять из 3 символов");
        try
        {
            
            var fromInformation = cTeleportService.GetAirportInformation(from);
            var toInformation = cTeleportService.GetAirportInformation(to);
            var result = await Task.WhenAll(fromInformation, toInformation);

            var distance = result[0].DistanceTo(result[1].Location);
            return Ok(distance);
        }
        catch(ArgumentException ex)
        {
            return NotFound(ex.Message);
        }

    }
}
