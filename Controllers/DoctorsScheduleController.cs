using DotNetData_Lb3.Repos;
using Microsoft.AspNetCore.Mvc;
using DotNetData_Lb3.Models;
using System.Text;

namespace DotNetData_Lb3.Controllers
{
    public class DoctorsScheduleController : Controller
    {
        private DoctorsScheduleRepo scheduleRepo;
        private FunctionsRepo functionsRepo;
        private PatientsRepo patientsRepo;
        private DoctorsRepo doctorsRepo;
        public DoctorsScheduleController(DoctorsScheduleRepo _scheduleRepo, 
            FunctionsRepo _functionsRepo, PatientsRepo _patientsRepo, DoctorsRepo _doctorsRepo)
        {
            scheduleRepo = _scheduleRepo;
            functionsRepo = _functionsRepo;
            patientsRepo = _patientsRepo;
            doctorsRepo = _doctorsRepo;
        }

        [HttpGet("schedules")]
        public async Task<IActionResult> GetSchedules()
        {
            List<DoctorsSchedule> schedules = await scheduleRepo.GetDoctorsSchedules();
            return View("Schedules", schedules);
        }

        [HttpGet("procedures")]
        public async Task<IActionResult> GetProcedures()
        {
            List<Patient> patients = await patientsRepo.GetPatients();
            List<Doctor> doctors = await doctorsRepo.GetDoctors();
            ViewBag.Doctors = doctors;
            ViewBag.Patients = patients;

            return View("Procedures");
        }

        [HttpPost("schedules/add")]
        public async Task<IActionResult> InsertSchedules([FromForm] DoctorsSchedule ds)
        {
            try
            {
                await scheduleRepo.InsertNewSchedule(ds);
                return RedirectToAction("GetSchedules");

            } catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                List<Patient> patients = await patientsRepo.GetPatients();
                List<Doctor> doctors = await doctorsRepo.GetDoctors();
                ViewBag.Doctors = doctors;
                ViewBag.Patients = patients;
                return View("Procedures");
            }
        }

        [HttpPost("procedures/topEarningDoctor")]
        public async Task<IActionResult> FindTopEarningDoctor([FromForm] DateTime date)
        {
            List<TopEarningDoctor> topEarningDoctors = functionsRepo.GetTopEarningDoctors(date);
            List<Patient> patients = await patientsRepo.GetPatients();
            List<Doctor> doctors = await doctorsRepo.GetDoctors();
            ViewBag.Doctors = doctors;
            ViewBag.Patients = patients;
            ViewData["TopEarningDoctors"] = topEarningDoctors;
            return View("Procedures");
        }

        [HttpPost("procedures/getSpentByPatient")]
        public async Task<IActionResult> GetSpentByPatient([FromForm] string phoneNumber)
        {
            if (phoneNumber is null)
                return RedirectToAction("GetProcedures");

            List<SpentByPatient> spent = await functionsRepo.GetSpentByPatient(phoneNumber);
            List<Patient> patients = await patientsRepo.GetPatients();
            List<Doctor> doctors = await doctorsRepo.GetDoctors();
            ViewBag.Doctors = doctors;
            ViewBag.Patients = patients;
            ViewData["SpentByPatient"] = spent;
            return View("Procedures");
        }

        [HttpPost("procedures/removeSubstring")]
        public async Task<IActionResult> RemoveSubstring([FromForm] string? InputString, 
            int StartPosition, int LengthToRemove)
        {
            List<Patient> patients = await patientsRepo.GetPatients();
            List<Doctor> doctors = await doctorsRepo.GetDoctors();
            ViewBag.Doctors = doctors;
            ViewBag.Patients = patients;
            string? res = await functionsRepo.RemoveSubstring(InputString, StartPosition, LengthToRemove);
            ViewData["RemoveSubstring"] = res;
            return View("Procedures");
        }
    }
}
