using APBD_5.DTOs;
using APBD_5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateRequestDto request)
    {
        try
        {
            var result = await _prescriptionService.CreatePrescriptionAsync(request);
            return CreatedAtAction(nameof(CreatePrescription), new { id = result }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}