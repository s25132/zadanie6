using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Model
{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public string Birthdate { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
