using APBD_5.DTOs;

namespace APBD_5.Repositories;

public interface IPatientRepository
{
    Task<PatientResponseDto?> GetPatientWithPrescriptionsAsync(int idPatient);
}