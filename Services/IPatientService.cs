using APBD_5.DTOs;

namespace APBD_5.Services;

public interface IPatientService
{
    Task<PatientResponseDto?> GetPatientAsync(int idPatient);
}