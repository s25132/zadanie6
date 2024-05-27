﻿using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Model
{
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string Type { get; set; }

        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
