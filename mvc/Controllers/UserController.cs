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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUserRepo _userRepo;

        public UserController(ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor, IUserRepo userRepo)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _userRepo = userRepo;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Index()
        {
            int userid = (int)_httpContextAccessor.HttpContext.Session.GetInt32("userid");
            if (userid == null)
            {
                return RedirectToAction("Login", "RegLogin");
            }
            ViewBag.userid = userid;
            return View();
        }

        [HttpGet]
        public IActionResult GetTripName(string type)
        {
            var tripnames = _userRepo.GetTripNames(type);
            return Ok(tripnames);
        }

        [HttpGet]
        public IActionResult GetTripDate(int name)
        {
            var dates = _userRepo.GetTripDate(name);
            return Ok(dates);
        }

        [HttpGet]
        public IActionResult GetTripPrice(int name, string date)
        {
            var price = _userRepo.GetTripPrice(name, date);
            return Ok(price);
        }

        [HttpPost]
        public IActionResult registerTrip(RegisterTrip registerTrip)
        {
            if (_userRepo.seatValidation(registerTrip))
            {
                if (_userRepo.registerTrip(registerTrip))
                {
                    return Ok();
                }
                return BadRequest(new { Response = "Not Registered!" });
            }
            else
            {
                return BadRequest(new { Response = "Seat not available!!" });
            }

        }

        // [HttpPost]
        // public IActionResult 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}