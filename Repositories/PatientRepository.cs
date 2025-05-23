using APBD_5.Data;
using APBD_5.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APBD_5.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<PatientRepository> _logger;

    public PatientRepository(AppDbContext context, ILogger<PatientRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PatientResponseDto?> GetPatientWithPrescriptionsAsync(int idPatient)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var patient = await _context.Patients
                .AsNoTracking()
                .Where(p => p.IdPatient == idPatient)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.Doctor)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.PrescriptionMedicaments)
                        .ThenInclude(pm => pm.Medicament)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (patient is null)
            {
                _logger.LogWarning("Patient with ID {IdPatient} not found.", idPatient);
                return null;
            }

            await transaction.CommitAsync();

            return new PatientResponseDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Prescriptions = patient.Prescriptions
                    .OrderBy(p => p.DueDate)
                    .Select(p => new PrescriptionResponseDto
                    {
                        IdPrescription = p.IdPrescription,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = p.Doctor.IdDoctor,
                            FirstName = p.Doctor.FirstName,
                            LastName = p.Doctor.LastName,
                            Email = p.Doctor.Email
                        },
                        Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentInfoDto
                        {
                            IdMedicament = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Medicament.Description,
                            Dose = pm.Dose,
                            Details = pm.Details
                        }).ToList()
                    }).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching patient with ID {IdPatient}", idPatient);
            await transaction.RollbackAsync();
            throw;
        }
    }
}
