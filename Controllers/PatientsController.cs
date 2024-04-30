using DotNetData_Lb3.Repos;
using Microsoft.AspNetCore.Mvc;
using DotNetData_Lb3.Models;

namespace DotNetData_Lb3.Controllers
{
    public class PatientsController : Controller
    {
        private PatientsRepo patientsRepo;
        public PatientsController(PatientsRepo _patientsRepo)
        {
            patientsRepo = _patientsRepo;
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetPatients(int? min = null, int? max = null)
        {
            List<Patient> patients = await patientsRepo.GetPatients(min, max);
            ViewBag.AgeBoundaries = patientsRepo.GetPatientAgeBoundaries();
            return View("Patients", patients);
        }

        [HttpPost("patients/add")]
        public async Task<IActionResult> InsertPatient([FromForm] Patient p)
        {
            await patientsRepo.InsertNewPatient(p);
            return RedirectToAction("GetPatients");
        }

        [HttpPost("patients/del")]
        public async Task<IActionResult> DelPatient([FromForm] string phoneNumber)
        {
            await patientsRepo.DeletePatient(phoneNumber);
            return RedirectToAction("GetPatients");
        }

        [HttpPost("patients/updatePatient")]
        public async Task<IActionResult> UpdatePatient(string patientId, Patient p)
        {
            await patientsRepo.UpdatePatient(patientId, p);

            return RedirectToAction("GetPatients");
        }

        [HttpPost("patients/search")]
        public async Task<IActionResult> SearchPatient(string name)
        {
            List<Patient> patients = await patientsRepo.SearchPatientsByName(name);
            ViewBag.AgeBoundaries = patientsRepo.GetPatientAgeBoundaries();
            return View("Patients", patients);
        }
    }
}
