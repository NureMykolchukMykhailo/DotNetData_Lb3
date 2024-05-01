using DotNetData_Lb3.Repos;
using Microsoft.AspNetCore.Mvc;
using DotNetData_Lb3.Models;
using MongoDB.Bson;
using MongoDB.Driver;


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
        public async Task<IActionResult> GetAppointments(List<string>? appointmentTypes = null,
            List<ObjectId>? doctors = null, List<ObjectId>? patients = null)
        {
            List<AppointmentFull> appointments = await appointmentsRepo.GetAppointments(appointmentTypes, doctors, patients);
            ViewBag.Doctors = await doctorsRepo.GetDoctors();
            ViewBag.Patients = await patientsRepo.GetPatients();
            return View("Appointments", appointments);
        }

        [HttpPost("appointments/add")]
        public async Task<IActionResult> InsertAppointment([FromForm] string PatientId, string DoctorId, Appointment a)
        {
            a.DoctorId = new ObjectId(DoctorId);
            a.PatientId = new ObjectId(PatientId);
            Console.WriteLine(a.DoctorId);
            Console.WriteLine(a.PatientId);
            await appointmentsRepo.InsertNewAppointment(a);
            return RedirectToAction("GetAppointments");
        }

        [HttpPost("appointments/del")]
        public async Task<IActionResult> DelAppointment([FromForm] string id)
        {
            await appointmentsRepo.DeleteAppointment(id);
            return RedirectToAction("GetAppointments");
        }

        [HttpPost("appointments/update")]
        public async Task<IActionResult> UpdateAppointment(string id, string PatientId, string DoctorId, Appointment a)
        {
            a.DoctorId = new ObjectId(DoctorId);
            a.PatientId = new ObjectId(PatientId);
            Console.WriteLine(a.DoctorId);
            Console.WriteLine(a.PatientId);
            await appointmentsRepo.UpdateAppointment(id, a);

            return RedirectToAction("GetAppointments");
        }
    }
}
