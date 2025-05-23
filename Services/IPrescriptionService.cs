using APBD_5.DTOs;

namespace APBD_5.Services;

public interface IPrescriptionService
{
    Task<int> CreatePrescriptionAsync(PrescriptionCreateRequestDto request);
}