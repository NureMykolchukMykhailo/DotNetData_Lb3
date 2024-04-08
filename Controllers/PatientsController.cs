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
        public async Task<IActionResult> GetPatients()
        {
            List<Patient> patients = await patientsRepo.GetPatients();
            return View("Patients", patients);
        }

        [HttpPost("patients/add")]
        public async Task<IActionResult> InsertPatient([FromForm] Patient p)
        {
            await patientsRepo.InsertNewPatient(p);
            return RedirectToAction("GetPatients");
        }
    }
}
