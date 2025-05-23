using System.ComponentModel.DataAnnotations;

namespace APBD_5.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public DateOnly DueDate { get; set; }
    
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public Patient Patient { get; set; } = null!;
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public Doctor Doctor { get; set; } = null!;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}