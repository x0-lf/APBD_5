namespace APBD_5.DTOs;

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }

    public List<PrescriptionResponseDto> Prescriptions { get; set; } = new();
}