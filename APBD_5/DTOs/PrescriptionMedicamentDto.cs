namespace APBD_5.DTOs;

public class PrescriptionMedicamentDto
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; } = string.Empty;
}