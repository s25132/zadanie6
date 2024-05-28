using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Model
{
    public class PatientReturnDto
    {
        [Required]
        public int IdPatient { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public List<PrescriptionDto> Prescriptions { get; set; }
    }
}
