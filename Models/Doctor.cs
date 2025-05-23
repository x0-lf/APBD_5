using System.ComponentModel.DataAnnotations;

namespace APBD_5.Models;

public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }

    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = string.Empty;

    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
