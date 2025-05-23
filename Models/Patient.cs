using System.ComponentModel.DataAnnotations;

namespace APBD_5.Models;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateOnly BirthDate { get; set; }

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
