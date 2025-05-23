using APBD_5.DTOs;
using APBD_5.Repositories;
using Microsoft.Extensions.Logging;

namespace APBD_5.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ILogger<PatientService> _logger;

    public PatientService(IPatientRepository patientRepository, ILogger<PatientService> logger)
    {
        _patientRepository = patientRepository;
        _logger = logger;
    }

    public async Task<PatientResponseDto?> GetPatientAsync(int idPatient)
    {
        try
        {
            return await _patientRepository.GetPatientWithPrescriptionsAsync(idPatient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving patient with ID: {Id}", idPatient);
            throw;
        }
    }
}