using DotNetData_Lb3.Repos;
using Microsoft.AspNetCore.Mvc;
using DotNetData_Lb3.Models;

namespace DotNetData_Lb3.Controllers
{
    public class AppointmentController : Controller
    {
        private AppointmentsRepo appointmentsRepo;
        private DoctorsRepo doctorsRepo;
        private PatientsRepo patientsRepo;
        public AppointmentController(AppointmentsRepo _appointmentsRepo, 
            DoctorsRepo _doctorsRepo, PatientsRepo _patientsRepo)
        {
            appointmentsRepo = _appointmentsRepo;
            doctorsRepo = _doctorsRepo;
            patientsRepo = _patientsRepo;
        }

        [HttpGet("appointments")]
        public async Task<IActionResult> GetAppointments()
        {
            List<Appointment> appointments = await appointmentsRepo.GetAppointments();
            ViewBag.Doctors = await doctorsRepo.GetDoctors();
            ViewBag.Patients = await patientsRepo.GetPatients();
            return View("Appointments", appointments);
        }

        [HttpPost("appointments/add")]
        public async Task<IActionResult> InsertAppointment([FromForm] Appointment a)
        {
            await appointmentsRepo.InsertNewAppointment(a);
            return RedirectToAction("GetAppointments");
        }
    }
}
