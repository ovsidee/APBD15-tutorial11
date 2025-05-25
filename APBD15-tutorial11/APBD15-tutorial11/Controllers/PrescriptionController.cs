using APBD15_tutorial11.Models.DTO;
using APBD15_tutorial11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD15_tutorial11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _dbService.AddPrescriptionAsync(request, cancellationToken);
        return result switch
        {
            "success" => Created(string.Empty, "Prescription added successfully."),
            "medicamentsCountExceeded" => BadRequest("A prescription cannot contain more than 10 medicaments."),
            "dueDateBeforeDate" => BadRequest("The due date must be later than or equal to the prescription date."),
            "doctorNotFound" => NotFound("The specified doctor does not exist."),
            "medicamentsNotFound" => NotFound("One or more medicaments were not found."),
            "patientDataMismatch" => Conflict("The patient data does not match the prescription."),
            _ => StatusCode(500, "An unexpected error occurred.")
        };
    }

}