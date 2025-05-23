using APBD_5.Models;

namespace APBD_5.Data;

public static class Seeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Doctors.Any() || context.Patients.Any() || context.Medicaments.Any())
            return;

        var doctors = new List<Doctor>
        {
            new Doctor { FirstName = "Maja", LastName = "Pszczolka", Email = "pszczolka.maja1337@gmail.com" },
            new Doctor { FirstName = "Piotr", LastName = "Efe", Email = "piotrek13@wp.pl" },
            new Doctor { FirstName = "Ola", LastName = "Bebe", Email = "OBebe@hotmail.com" }
        };

        var patients = new List<Patient>
        {
            new Patient { FirstName = "Jan", LastName = "Niezbedny", BirthDate = new DateOnly(1969, 5, 5) },
            new Patient { FirstName = "Zofia", LastName = "Samosia", BirthDate = new DateOnly(1996, 6, 22) },
            new Patient { FirstName = "Tomasz", LastName = "Wetter", BirthDate = new DateOnly(2002, 1, 15) }
        };

        var medicaments = new List<Medicament>
        {
            new Medicament { Name = "Ibuprofen", Description = "NSAID Ibuprofen Tablets", Type = "Tablet" },
            new Medicament { Name = "Ketoprofen", Description = "NSAID Ketoprofen Capsules", Type = "Capsule" },
            new Medicament { Name = "Paracetamol", Description = "NSAID Paracetamol Tablets", Type = "Tablet" },
            new Medicament { Name = "Naproxen", Description = "NSAID Naproxen Tablets", Type = "Tablet" },
            new Medicament { Name = "Oxycodone Hydrochloride Wockhardt", Description = "Cough Suppressant", Type = "Syrup" },
            new Medicament { Name = "Metamizole", Description = "Analgesic Metamizole", Type = "Tablet" },
            new Medicament { Name = "Aspirin", Description = "Acetylsalicylic acid", Type = "Tablet" },
            new Medicament { Name = "Dextromethorphan", Description = "Cough Suppressant", Type = "Syrup" },
            new Medicament { Name = "Loratadine", Description = "Antihistamine", Type = "Tablet" },
            new Medicament { Name = "Lisinopril", Description = "ACE Inhibitor", Type = "Capsule" },
            new Medicament { Name = "Penicillin", Description = "Antibiotic", Type = "Capsule" }
        };

        context.Doctors.AddRange(doctors);
        context.Patients.AddRange(patients);
        context.Medicaments.AddRange(medicaments);
        context.SaveChanges();

        var prescriptions = new List<Prescription>
        {
            new Prescription
            {
                Date = new DateOnly(2023, 10, 1),
                DueDate = new DateOnly(2023, 10, 15),
                Doctor = doctors[0],
                Patient = patients[0],
                PrescriptionMedicaments = new List<PrescriptionMedicament>
                {
                    new() { Medicament = medicaments[0], Dose = 2, Details = "Twice a day" },
                    new() { Medicament = medicaments[2], Dose = 1, Details = "After breakfast" }
                }
            },
            new Prescription
            {
                Date = new DateOnly(2023, 11, 5),
                DueDate = new DateOnly(2023, 11, 20),
                Doctor = doctors[1],
                Patient = patients[1],
                PrescriptionMedicaments = new List<PrescriptionMedicament>
                {
                    new() { Medicament = medicaments[1], Dose = 3, Details = "Before meals" }
                }
            },
            new Prescription
            {
                Date = new DateOnly(2023, 12, 1),
                DueDate = new DateOnly(2023, 12, 31),
                Doctor = doctors[2],
                Patient = patients[2],
                PrescriptionMedicaments = new List<PrescriptionMedicament>
                {
                    new() { Medicament = medicaments[0], Dose = 1, Details = "Morning only" },
                    new() { Medicament = medicaments[1], Dose = 1, Details = "Evening only" }
                }
            }
        };

        context.Prescriptions.AddRange(prescriptions);
        context.SaveChanges();
    }
}
