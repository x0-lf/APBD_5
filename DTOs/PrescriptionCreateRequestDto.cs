namespace APBD_5.DTOs;

public class PrescriptionCreateRequestDto
{
    public PatientDto Patient { get; set; } = null!;
    public int IdDoctor { get; set; }
    public List<PrescriptionMedicamentDto> Medicaments { get; set; } = new List<PrescriptionMedicamentDto>();
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}