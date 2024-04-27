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
            return View("Doctors", doctors);
        }


        //[HttpPost("doctors/add")]
        //public async Task<IActionResult> InsertDoctor([FromForm] Doctor d)
        //{
        //    await doctorsRepo.InsertNewDoctor(d);
        //    return RedirectToAction("GetDoctors");
        //}
    }
}
