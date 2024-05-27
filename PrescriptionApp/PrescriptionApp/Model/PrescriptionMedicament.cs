using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Model
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public Medicament Medicament { get; set; }

        public int IdPrescription { get; set; }
        public Prescription Prescription { get; set; }

        [Required]
        public int Dose { get; set; }
        [MaxLength(100)]
        public string Detalis { get; set; }
    }
}
