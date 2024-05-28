using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Model;

namespace PrescriptionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PrescriptionContext _context;

        public PatientController(PrescriptionContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(r => r.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                .ThenInclude(r => r.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == id);

            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            var result = new PatientReturnDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Prescriptions = patient.Prescriptions
                    .OrderBy(r => r.DueDate)
                    .Select(r => new PrescriptionDto
                    {
                        IdPrescription = r.IdPrescription,
                        Date = r.Date,
                        DueDate = r.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = r.Doctor.IdDoctor,
                            FirstName = r.Doctor.FirstName,
                            LastName = r.Doctor.LastName
                        }
                    }).ToList()
            };

            return Ok(result);
        }
    }
}
