using System.ComponentModel.DataAnnotations;

namespace APBD_5.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required, StringLength(100)]
    public string Description { get; set; } = string.Empty;
    
    [Required, StringLength(100)]
    public string Type { get; set; } = string.Empty;

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}
