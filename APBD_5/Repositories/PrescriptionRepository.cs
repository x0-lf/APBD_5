using APBD_5.Data;
using APBD_5.DTOs;
using APBD_5.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_5.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly AppDbContext _context;

    public PrescriptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreatePrescriptionAsync(PrescriptionCreateRequestDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Ensure doctor exists
            var doctor = await _context.Doctors.FindAsync(request.IdDoctor);
            if (doctor is null)
                throw new KeyNotFoundException("Doctor not found.");

            // Check if patient exists
            var patient = await _context.Patients.FirstOrDefaultAsync(p =>
                p.FirstName == request.Patient.FirstName &&
                p.LastName == request.Patient.LastName &&
                p.BirthDate == request.Patient.BirthDate);

            if (patient is null)
            {
                patient = new Patient
                {
                    FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                    BirthDate = request.Patient.BirthDate
                };
                _context.Patients.Add(patient);
                // defer SaveChangesAsync
            }

            // Validate medicaments
            var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
            var foundMedicaments = await _context.Medicaments
                .Where(m => medicamentIds.Contains(m.IdMedicament))
                .Select(m => m.IdMedicament)
                .ToListAsync();

            if (foundMedicaments.Count != medicamentIds.Count)
                throw new KeyNotFoundException("One or more medicaments do not exist.");

            // Create prescription
            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                Doctor = doctor,
                Patient = patient,
                PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    Dose = m.Dose,
                    Details = m.Details
                }).ToList()
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return prescription.IdPrescription;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
