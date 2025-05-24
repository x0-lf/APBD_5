namespace APBD_5.DTOs;


public class PrescriptionResponseDto
{
    public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }

    public DoctorDto Doctor { get; set; } = null!;
    public List<MedicamentInfoDto> Medicaments { get; set; } = new List<MedicamentInfoDto>();
}