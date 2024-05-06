using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;
using mvc.Repository.Interface;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminRepo _adminRepo;

        public AdminController(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        //fetch all trip
        public IActionResult GetAllTrip()
        {
            var trips = _adminRepo.FetchAllTrip();
            return Json(trips);
        }

        //fetch trip names
        public IActionResult FetchTripName()
        {
            var tripname = _adminRepo.FetchAllTripNames();
            return Json(tripname);
        }

        //add trip
        [HttpPost("/AddTrip")]
        public IActionResult AddTrip(Trip addtrip)
        {
            _adminRepo.AddTrip(addtrip);
            return Ok();
        }

        [HttpPost("/UpdateTrip")]
        public IActionResult UpdateTrip(Trip updatetrip)
        {
            _adminRepo.UpdateTrip(updatetrip);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteTrip(int id)
        {
            _adminRepo.DeleteTrip(id);
            return Ok();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}