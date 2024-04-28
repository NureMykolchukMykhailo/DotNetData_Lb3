using DotNetData_Lb3.Repos;
using Microsoft.AspNetCore.Mvc;
using DotNetData_Lb3.Models;

namespace DotNetData_Lb3.Controllers
{

    public class DoctorController : Controller
    {
        private DoctorsRepo doctorsRepo;
        public DoctorController(DoctorsRepo _doctorsRepo)
        {
            doctorsRepo = _doctorsRepo;
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors(List<string>? specialties = null, List<string>? daysOfWeek = null)
        {
            List<Doctor> doctors = await doctorsRepo.GetDoctors(specialties, daysOfWeek);
            ViewBag.Specialties = await doctorsRepo.GetDoctorsSpecialties();
            return View("Doctors", doctors);
        }

        [HttpPost("doctors/add")]
        public async Task<IActionResult> InsertDoctor([FromForm] Doctor d)
        {
            try
            {
                await doctorsRepo.InsertNewDoctor(d);
            }
            catch (Exception ex)
            {
                TempData.Add("Exeption", ex.Message);
                return RedirectToAction("GetDoctors");
            }
            return RedirectToAction("GetDoctors");
        }

        [HttpPost("doctor/del")]
        public async Task<IActionResult> DelDoctor([FromForm] string phoneNumber)
        {
            
            await doctorsRepo.DeleteDoctor(phoneNumber);
            
            return RedirectToAction("GetDoctors");
        }

        [HttpPost("doctor/updateSchedule")]
        public async Task<IActionResult> UpdateDoctorSchedule(string phoneNumber, Schedule s)
        {

            await doctorsRepo.UpdateDoctorSchedule(phoneNumber, s);

            return RedirectToAction("GetDoctors");
        }

    }
}
