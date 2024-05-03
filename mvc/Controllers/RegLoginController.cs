using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;

namespace mvc.Controllers
{
    // [Route("[controller]")]
    public class RegLoginController : Controller
    {
        private readonly ILogger<RegLoginController> _logger;

        public RegLoginController(ILogger<RegLoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(RegLoginModel login){
            return View();
        }

        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegLoginModel register){
            return View(register);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}