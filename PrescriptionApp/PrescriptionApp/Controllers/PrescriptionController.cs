using Microsoft.AspNetCore.Mvc;
using PrescriptionApp.Model;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PrescriptionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly PrescriptionContext _context;

        public PrescriptionController(PrescriptionContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPrescription([FromBody] PrescriptionDto dto)
        {
            if (dto.Medicaments.Count > 10)
            {
                return BadRequest("Medicaments limit exceeded.");
            }

            if (dto.DueDate < dto.Date)
            {
                return BadRequest("DueDate must be greater than Date.");
            }

            var patient = await _context.Patients.FindAsync(dto.Patient.IdPatient);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = dto.Patient.FirstName,
                    LastName = dto.Patient.LastName
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            var doctor = await _context.Doctors.FindAsync(dto.Doctor.IdDoctor);
            if (doctor == null)
            {
                return BadRequest("Doctor does not exist.");
            }

            foreach (var medicament in dto.Medicaments)
            {
                var med = await _context.Medicaments.FindAsync(medicament.IdMedicament);
                if (med == null)
                {
                    return BadRequest($"Medicament with Id {medicament.IdMedicament} does not exist.");
                }
            }

            var prescription = new Prescription
            {
                Date = dto.Date,
                DueDate = dto.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = doctor.IdDoctor
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            foreach (var medicament in dto.Medicaments)
            {
                var prescriptionMedicament = new PrescriptionMedicament
                {
                    IdPrescription = prescription.IdPrescription,
                    IdMedicament = medicament.IdMedicament,
                    Dose = medicament.Dose,
                    Detalis = medicament.Description 
                };
                _context.PrescriptionMedicaments.Add(prescriptionMedicament);
            }

            await _context.SaveChangesAsync();

            return Ok(prescription.IdPrescription);
        }
    }
}
