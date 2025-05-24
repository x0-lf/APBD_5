using APBD_5.DTOs;
using APBD_5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PatientResponseDto>> GetPatient(int id)
    {
        var patient = await _patientService.GetPatientAsync(id);
        if (patient is null)
            return NotFound();

        return Ok(patient);
    }
}