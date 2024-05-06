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
        public IActionResult AddTrip(Trip addtrip, IFormFile file)
        {
            //image
            if (file != null && file.Length > 0)
            {
                var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(folderpath))
                {
                    Directory.CreateDirectory(folderpath);
                }
                var filename = Path.GetFileName(file.FileName);
                var filepath = Path.Combine(folderpath, filename);
                var stream = new FileStream(filepath, FileMode.Create);
                file.CopyTo(stream);
                var imgurl = "/images/" + filename;
                addtrip.c_image = imgurl;
            }

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