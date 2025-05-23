using APBD_5.DTOs;

namespace APBD_5.Repositories;

public interface IPrescriptionRepository
{
    Task<int> CreatePrescriptionAsync(PrescriptionCreateRequestDto request);
}