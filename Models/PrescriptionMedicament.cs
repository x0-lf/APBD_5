using System.ComponentModel.DataAnnotations;

namespace APBD_5.Models;

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; } = null!;

    [Required, StringLength(100)]
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; } = null!;

    public int? Dose { get; set; }

    [Required, StringLength(100)]
    public string Details { get; set; } = string.Empty;
}
