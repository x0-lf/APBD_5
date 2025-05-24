using APBD_5.Data;
using APBD_5.DTOs;
using APBD_5.Models;
using APBD_5.Repositories;
using Microsoft.Extensions.Logging;

namespace APBD_5.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly ILogger<PrescriptionService> _logger;

    public PrescriptionService(IPrescriptionRepository prescriptionRepository, ILogger<PrescriptionService> logger)
    {
        _prescriptionRepository = prescriptionRepository;
        _logger = logger;
    }

    public async Task<int> CreatePrescriptionAsync(PrescriptionCreateRequestDto request)
    {
        try
        {
            if (request.Medicaments.Count > 10)
                throw new ArgumentException("Prescription cannot contain more than 10 medicaments.");

            if (request.DueDate < request.Date)
                throw new ArgumentException("DueDate must be greater than or equal to Date.");

            return await _prescriptionRepository.CreatePrescriptionAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create prescription for patient: {Patient}", request.Patient);
            throw;
        }
    }
}