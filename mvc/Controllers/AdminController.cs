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
        public IActionResult GetAllTrip(int pagenumber = 1, int pageSize = 10)
        {
            var trips = _adminRepo.FetchAllTrip(pagenumber, pageSize);
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
            var trip = _adminRepo.GetOneTrip(updatetrip.c_id);
            if (updatetrip.c_image == null || updatetrip.c_image == "")
            {
                updatetrip.c_image = trip.c_image;
            }
            _adminRepo.UpdateTrip(updatetrip);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteTrip(int id)
        {
            _adminRepo.DeleteTrip(id);
            return Ok();
        }


        [HttpPost]
        public IActionResult UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            return Ok();
        }

        public IActionResult ShowAllBookTicket()
        {
            return View();
        }

        public IActionResult FetchAllTicket()
        {
            var ticket = _adminRepo.GetAllBookTicket();
            return Ok(ticket);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}