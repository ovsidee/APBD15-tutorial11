using Microsoft.AspNetCore.Mvc;
using APBD15_tutorial11.Services;

namespace APBD15_tutorial11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var patient = await _dbService.GetPatientDetailsAsync(id);

        if (patient == null)
        {
            return NotFound($"Patient with ID {id} not found.");
        }

        return Ok(patient);
    }
}
