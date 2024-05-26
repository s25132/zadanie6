namespace PrescriptionApp.Model
{
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public Medicament Medicament { get; set; }

        public int IdPrescription { get; set; }
        public Prescription Prescription { get; set; }
    }
}
