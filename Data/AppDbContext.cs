using Microsoft.EntityFrameworkCore;
using APBD_5.Models;

namespace APBD_5.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Medicament> Medicaments => Set<Medicament>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments => Set<PrescriptionMedicament>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table names
        modelBuilder.Entity<Patient>().ToTable("Patient");
        modelBuilder.Entity<Doctor>().ToTable("Doctor");
        modelBuilder.Entity<Medicament>().ToTable("Medicament");
        modelBuilder.Entity<Prescription>().ToTable("Prescription");
        modelBuilder.Entity<PrescriptionMedicament>().ToTable("Prescription_Medicament");

        // Composite key for join table
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        // Relationships
        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdPatient)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(p => p.IdDoctor)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        // Additional Column constraints
        modelBuilder.Entity<Medicament>()
            .Property(m => m.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<PrescriptionMedicament>()
            .Property(pm => pm.Details)
            .HasMaxLength(100)
            .IsRequired();
    }
}
