using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Model
{
    public class DoctorDto
    {
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
    }
}
