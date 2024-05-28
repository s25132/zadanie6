using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Model
{
    public class PrescriptionDto
    {
        [Required]
        public int IdPrescription { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }
        [Required]
        public PatientDto Patient { get; set; }
        [Required]
        public DoctorDto Doctor { get; set; }
        [Required]
        public List<MedicamentDto> Medicaments { get; set; }
    }
}
